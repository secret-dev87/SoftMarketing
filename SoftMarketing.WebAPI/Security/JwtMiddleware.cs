using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SoftMarketing.Model;
using SoftMarketing.Model.MarketingModels;
using SoftMarketing.Model.SalesModels;
using SoftMarketing.WebAPI.Helpers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

namespace SoftMarketing.WebAPI.Security
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        IOptions<AppSettings> _appSettings2;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IUserManager UserManager, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            //var rTokend = context.Request.Headers["refreshToken"].FirstOrDefault();
            //var rtoken = context.Request.Headers["Cookie"].FirstOrDefault();
            var authReq = jwtUtils.ValidateJwtToken(token);

            User claimUser = null;
            //check if token revoked or not 
            if (authReq != null && authReq.UserId != null)
            {
                //var rToken = context.Request.Cookies["refreshToken"];
                var rToken = context.Request.Headers["refreshToken"].FirstOrDefault();
                if (!string.IsNullOrEmpty(rToken))
                {
                    //var claimUser = UserService.GetById(authReq.UserId);
                    claimUser = UserManager.GetSalesUserAndRefreshTokensByUserId(authReq.UserId);

                    var refreshToken = claimUser.RefreshTokens?.FirstOrDefault(e => e.Token == rToken);
                    if (!refreshToken.IsActive)
                        throw new AppException("Invalid token");
                    if (refreshToken == null)
                    {
                        throw new AppException("Unauthorized");
                    }
                    if (claimUser.phone_countryId == authReq.CountryId && claimUser.phone == authReq.Phone)
                        context.Items["User"] = claimUser;
                }
            }

            var syncFlag = context.Request.Headers["SyncFlag"].FirstOrDefault();

            if (claimUser != null && syncFlag != null && syncFlag == "1")
            {
                var originBody = context.Response.Body;
                try
                {
                    var memStream = new MemoryStream();
                    context.Response.Body = memStream;
                    await _next(context).ConfigureAwait(false);
                    memStream.Position = 0;
                    var responseBody = new StreamReader(memStream).ReadToEnd();
                    var desResponse = JsonConvert.DeserializeObject<dynamic>(responseBody)!;
                    try { desResponse.SyncedData = Helper.Compress(JsonConvert.SerializeObject(SyncData(context, claimUser, UserManager))); }
                    catch (Exception) { }
                    var serResponse = JsonConvert.SerializeObject(desResponse);
                    var memoryStreamModified = new MemoryStream();
                    var sw = new StreamWriter(memoryStreamModified);
                    sw.Write(serResponse);
                    sw.Flush();
                    memoryStreamModified.Position = 0;
                    await memoryStreamModified.CopyToAsync(originBody).ConfigureAwait(false);
                }
                finally
                {
                    context.Response.Body = originBody;
                }
            }
            else
            {
                await _next(context);
            }
        }

        SyncedData SyncData(HttpContext context, User user, IUserManager userManager)
        {
            var last_update_customer = context.Request.Headers["last_update_customer"].FirstOrDefault();
            var last_update_message = context.Request.Headers["last_update_message"].FirstOrDefault();
            var last_update_setting = context.Request.Headers["last_update_setting"].FirstOrDefault();
            var last_update_template = context.Request.Headers["last_update_template"].FirstOrDefault();
            var last_update_template_date = context.Request.Headers["last_update_template_date"].FirstOrDefault();
            //if (last_update_customer != null || last_update_message != null || last_update_setting != null || last_update_template != null)
            //{
            List<string> tables = new List<string>();
            //if (last_update_customer != null)
            //{
            if (last_update_customer != user.last_update_customer)
            {
                tables.Add("customer");
            }
            //}
            //if (last_update_message != null)
            //{
            if (last_update_message != user.last_update_message)
            {
                tables.Add("message");
            }
            //}
            //if (last_update_setting != null)
            //{
            if (last_update_setting != user.last_update_setting)
            {
                tables.Add("setting");
            }
            //}
            //if (last_update_template != null)
            //{
            if (last_update_template != user.last_update_template)
            {
                tables.Add("template");
            }
            //}
            //if (last_update_template_date != null)
            //{
            if (last_update_template_date != user.last_update_template_date)
            {
                tables.Add("template-date");
            }
            //}
            var syncedData = userManager.Sync(user, tables);
            return syncedData;
        }
    }

}
