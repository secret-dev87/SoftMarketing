using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SoftMarketing.Model;
using SoftMarketing.Model.SalesModels;
using SoftMarketing.Services.Sales;
using SoftMarketing.WebAPI.Helpers;
using SoftMarketing.WebAPI.Security;

namespace SoftMarketing.WebAPI
{
    public class ServerHub : Hub
    {
        IJwtUtils JwtUtils { set; get; }
        private readonly IHubContext<ServerHub> hubContext;
        public ServerHub(IJwtUtils _JwtUtils = null, IHubContext<ServerHub> _hubContext = null)
        {
            JwtUtils = _JwtUtils;
            hubContext = _hubContext;
        }
        public override Task OnConnectedAsync()
        {
            var result = is_trusted();
            if (result.Item1 && !string.IsNullOrEmpty(result.Item2))
            {
                hubContext.Groups.AddToGroupAsync(Context.ConnectionId, result.Item2);
                return base.OnConnectedAsync();
            }
            else
            {
                throw new HubException("Unauthorized");
            }
        }

        private Tuple<bool, string?> is_trusted()
        {
            try
            {
                var headers = Context?.GetHttpContext()?.Request.Headers;
                var countryId = headers?["cid"].ToString();
                var userId = headers?["uid"].ToString();
                var phoneNumber = headers?["phnum"].ToString();
                var psd = headers?["psd"].ToString();
                if (!string.IsNullOrEmpty(phoneNumber) && !string.IsNullOrEmpty(countryId))
                {
                    var user = new UserService().Get(phoneNumber, Convert.ToInt32(countryId));
                    
                    if (!(user == null || psd == null || !Helper.VerifyPassword(psd, user.hash_password, user.salt) || user.user_statusId != (int)UserStatus.active))
                    {
                        return Tuple.Create(true, userId);
                    }
                }
            }
            catch (Exception)
            {
                return Tuple.Create(false, string.Empty);
            }
            return Tuple.Create(false, string.Empty);
        }


        public void UpdateClientData(string proccessType, User user, object data)
        {
            var serelizedData = JsonConvert.SerializeObject(data);
            hubContext.Clients.Group(user.id.ToString()).SendAsync(proccessType, serelizedData);
        }
    }
}
