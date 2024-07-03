//using Newtonsoft.Json;
//using SoftMarketing.Model;
//using SoftMarketing.Model.SalesModels;
//using SoftMarketing.WebAPI.Security;
//using System.IO.Compression;
//using System.Net;
//using System.Text;
//using System.Text.Json;
//namespace SoftMarketing.WebAPI.Helpers
//{

//    public class ErrorHandlerMiddleware
//    {
//        private readonly RequestDelegate _next;

//        public ErrorHandlerMiddleware(RequestDelegate next)
//        {
//            _next = next;
//        }

//        //public void SetContext(HttpContext context)
//        //{
//        //    context.Response.Headers.Add()
//        //}
//        public async Task Invoke(HttpContext context, IUserManager UserManager)
//        {
//            try
//            {
//                await _next(context);
//            }
//            catch (Exception error)
//            {
//                var response = context.Response;
//                response.ContentType = "application/json";

//                //if(error is NotSynced)
//                //{
//                //    var syncedData = (NotSynced)error;
//                //    //SetContext(context);
//                //    var result = new Response<string>();
//                //    result.ErrorMessage = "SyncData";
//                //    result.IsSuccess = false;
//                //    result.Item = Helper.Compress(JsonSerializer.Serialize(syncedData._syncedData));
//                //    var serilizedData = JsonSerializer.Serialize(result);
//                //    await response.WriteAsync(serilizedData);
//                //}
//                //else
//                //{
//                switch (error)
//                {
//                    case AppException e:
//                        // custom application error
//                        response.StatusCode = (int)HttpStatusCode.BadRequest;
//                        break;
//                    case KeyNotFoundException e:
//                        // not found error
//                        response.StatusCode = (int)HttpStatusCode.NotFound;
//                        break;
//                    default:
//                        // unhandled error
//                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
//                        break;
//                }
//                var syncFlag = context.Request.Headers["SyncFlag"].FirstOrDefault();
//                if (syncFlag != null && syncFlag == "1")
//                {
//                    var user = (User)context.Items["User"];
//                    var result = JsonConvert.SerializeObject(new { message = error?.Message, SyncedData = Helper.Compress(JsonConvert.SerializeObject(SyncData(context, user, UserManager))) });
//                    await response.WriteAsync(result);
//                }
//                else
//                {
//                    var result = JsonConvert.SerializeObject(new
//                    {
//                        message = error?.Message
//                    });
//                }
//            }

//            SyncedData SyncData(HttpContext context, User user, IUserManager userManager)
//            {
//                var last_update_customer = context.Request.Headers["last_update_customer"].FirstOrDefault();
//                var last_update_message = context.Request.Headers["last_update_message"].FirstOrDefault();
//                var last_update_setting = context.Request.Headers["last_update_setting"].FirstOrDefault();
//                var last_update_template = context.Request.Headers["last_update_template"].FirstOrDefault();
//                if (last_update_customer != null || last_update_message != null || last_update_setting != null || last_update_template != null)
//                {
//                    List<string> tables = new List<string>();
//                    if (last_update_customer != null)
//                    {
//                        if (Convert.ToDateTime(last_update_customer) != user.last_update_customer)
//                        {
//                            tables.Add("customer");
//                        }
//                    }
//                    if (last_update_message != null)
//                    {
//                        if (Convert.ToDateTime(last_update_message) != user.last_update_message)
//                        {
//                            tables.Add("message");
//                        }
//                    }
//                    if (last_update_setting != null)
//                    {
//                        if (Convert.ToDateTime(last_update_setting) != user.last_update_setting)
//                        {
//                            tables.Add("setting");
//                        }
//                    }
//                    if (last_update_template != null)
//                    {
//                        if (Convert.ToDateTime(last_update_template) != user.last_update_template)
//                        {
//                            tables.Add("template");
//                        }
//                    }
//                    var syncedData = userManager.Sync(user, tables);
//                    return syncedData;
//                }
//                return null;
//            }
//        }
//    }
//}
