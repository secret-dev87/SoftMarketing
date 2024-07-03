//using Microsoft.AspNetCore.Connections.Features;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.AspNetCore.SignalR;
//using Microsoft.Extensions.Options;
//using MySql.Data.MySqlClient;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using SoftMarketing.Model;
//using SoftMarketing.Model.SalesModels;
//using SoftMarketing.Services.Marketing;
//using SoftMarketing.Services.Sales;
//using SoftMarketing.WebAPI.Helpers;
//using SoftMarketing.WebAPI.Security;

//namespace SoftMarketing.WebAPI
//{
//    public class DataHub : Hub
//    {
//        IJwtUtils JwtUtils { set; get; }
//        IUserManager UserManager { set; get; }
//        private readonly IHubContext<DataHub> hubContext;
//        public DataHub(IJwtUtils _JwtUtils = null, IUserManager _UserManager = null, IHubContext<DataHub> _hubContext = null)
//        {
//            JwtUtils = _JwtUtils;
//            UserManager = _UserManager;
//            hubContext = _hubContext;
//        }

//        public async void conn_state(string state, string connId)
//        {
//            hubContext.Clients.Client(connId).SendAsync("conn_state", state);
//        }
        
//        public async void UpdateClientData(string proccessType, User user, object data, HttpContext context)
//        {
//            var serelizedData = JsonConvert.SerializeObject(data);
//            var app_source_id = Convert.ToInt16(context.Request.Headers["app_source_id"].FirstOrDefault());
//            //user = UserManager.GetSalesUserAndRefreshTokensByUserId(user.id);
//            switch (app_source_id)
//            {
//                case 1:
//                    {
//                        var connectedClients = user.RefreshTokens.Where(i => i.AppSourceId != 1)?.ToList();

//                        foreach (var rToken in connectedClients)
//                        {
//                            if (rToken.ConnectionId != null)
//                                await hubContext.Clients.Client(rToken.ConnectionId).SendAsync(proccessType, serelizedData);
//                        }
//                    }
//                    break;
//                case 2:
//                    {
//                        var connectedClients = user.RefreshTokens.Where(i => i.AppSourceId != 2)?.ToList();

//                        foreach (var rToken in connectedClients)
//                        {
//                            if (rToken.ConnectionId != null)
//                                await hubContext.Clients.Client(rToken.ConnectionId).SendAsync(proccessType, serelizedData);
//                        }
//                    }
//                    break;
//                case 3:
//                    {
//                        var connectedClients = user.RefreshTokens.Where(i => i.AppSourceId != 3)?.ToList();

//                        foreach (var rToken in connectedClients)
//                        {
//                            if (rToken.ConnectionId != null)
//                                await hubContext.Clients.Client(rToken.ConnectionId).SendAsync(proccessType, serelizedData);
//                        }
//                    }
//                    break;
//                default:
//                    break;
//            }
//        }
//        public override Task OnConnectedAsync()
//        {
//            if (is_trusted())
//            {
//                return base.OnConnectedAsync();
//            }
//            else
//            {
//                throw new HubException("Unauthorized");
//            }
//        }
//        public override Task OnDisconnectedAsync(Exception? exception)
//        {
//            var headers = Context.GetHttpContext().Request.Headers;
//            var uid = Convert.ToInt32(headers["uid"]);
//            var userService = new UserService();
//            var tokens = userService.GetRefreshTokensByUserIdAndConnectionId(uid, Context.ConnectionId);
//            if(tokens != null)
//            {
//                var token = tokens.FirstOrDefault();
//                if(token != null)
//                {
//                    token.ConnectionId = null;
//                    userService.UpdateRefreshToken(token);
//                }
//            }
//            return base.OnDisconnectedAsync(exception);
//        }

        
//        public Tuple<bool,string,int> authenticate(string str)
//        {  
//            var values = str.Split(" , ");
//            var rToken = Convert.ToString(values[0]);
//            var userId = Convert.ToInt32(values[1]);
//            var appSourceId = Convert.ToInt32(values[2]);
//            var jwt = Convert.ToString(values[3]);
//            var authRequest = JwtUtils.ValidateJwtToken(jwt);
//            var is_trusted = false;
//            var connId = Context.ConnectionId;
//            if (!string.IsNullOrEmpty(str) && authRequest != null && authRequest.UserId == userId
//                && authRequest.AppSourceId == appSourceId.ToString())
//            {
//                User claimUser = UserManager.GetSalesUserAndRefreshTokensByUserId(userId);
//                var refreshToken = claimUser.RefreshTokens?.FirstOrDefault(e => e.Token == rToken);
//                if (refreshToken != null && refreshToken.AppSourceId == appSourceId)
//                {
//                    refreshToken.ConnectionId = connId;
//                    var result = UserManager.UpdateHubConnections(refreshToken);
//                    if (result > 0)
//                    {
//                        is_trusted = true;
//                    }
//                }
//            }
//            return new Tuple<bool, string,int>(is_trusted, connId,userId);
//        }
//        public async Task Validate(string str)
//        {
//            var result = authenticate(str);
//            if (result.Item1)
//            {
//                conn_state("validated", result.Item2);
//            }
//            else
//            {
//                conn_state("not_validated", result.Item2);
//            }
//        }
//        private bool is_trusted()
//        {
//            bool is_trusted = false;
//            try
//            {
//                var headers = Context.GetHttpContext().Request.Headers;
//                var cid = headers["cid"].ToString();
//                var uid = headers["uid"].ToString();
//                var phnum = headers["phnum"].ToString();
//                var psd = headers["psd"].ToString();
//                var user = new UserService().Get(phnum, Convert.ToInt32(cid));

//                if (!(user == null || psd == null || !Helper.VerifyPassword(psd, user.hash_password, user.salt)))
//                {
//                    is_trusted = true;
//                }
//            }
//            catch (Exception)
//            {
//                return is_trusted;
//            }
//            return is_trusted;
//        }
//    }
//}
