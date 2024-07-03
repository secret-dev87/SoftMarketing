using System;
using System.Data;
using System.Data.Common;
using System.Collections;
//using SoftMarketing.Service;
using SoftMarketing.Model;
using SoftMarketing.WebAPI.Core;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using SoftMarketing.DAL.Helper;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using SoftMarketing.WebAPI.Security;
using Microsoft.Extensions.Options;
using SoftMarketing.Services.Sales;
using Microsoft.IdentityModel.Tokens;
using SoftMarketing.Model.SalesModels;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient.Memcached;
using Org.BouncyCastle.Asn1.Crmf;
using System.Net.Http.Headers;
using SoftMarketing.Model.DTOs;
using RestSharp;
using Newtonsoft.Json;
using SoftMarketing.WebAPI.Helpers;
using SoftMarketing.Model.MarketingModels;
using SoftMarketing.Service.Marketing;

namespace SoftMarketing.WebAPI
{

    /// <summary>
    /// 1- Authinticate request
    /// 2- when JWT expiered call refresh Token 
    /// to signout call revoke all tokens 
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ApiControllerBase
    {
        //HttpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString()
        //UserService userService { get; set; }
        SoftMarketing.WebAPI.Security.IUserManager UserManager { get; set; }
        IUserService UserService { get; set; }


        private readonly AppSettings AppSettings;
        //private readonly IConfiguration configuration;

        public UserController(IOptions<AppSettings> _AppSettings, IUserManager _UserManager, IUserService userService)
        {
            AppSettings = _AppSettings.Value;
            UserManager = _UserManager;
            UserService = userService;
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            AuthenticateResponse response = null;
            try
            {
                var appSourceId = HttpContext.Request.Headers["app_source_id"].FirstOrDefault();
                if (model.AppSourceId == appSourceId)
                {
                    model.AppSourceId = appSourceId;
                    response = UserManager.Authenticate(model, ipAddress());
                    //var refreshToken = Request.Cookies["refreshToken"];
                    //setTokenCookie(response.RefreshToken);
                    return Ok(response);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register(OBusiness_Webhook model)
        {
            var x = HttpContext;
            var hashPass = Helper.HashPasword(model.udf.snv, out var salt);
            int agentSalesId = 0;
            if (!string.IsNullOrEmpty(model.udf.seller_number))
            {
                agentSalesId = int.Parse(model.udf.seller_number);
            }
            var user = new User
            {
                phone = model.customer.contact_number,
                hash_password = hashPass,
                salt = salt,
                agent_sales_userId = agentSalesId,
                phone_countryId = model.udf.phone_countryId,
                name = model.udf.name,
                sales_planId = model.udf.planId,
                pt = model.id
            };
            var id = UserService.Register(user);
            ////var refreshToken = Request.Cookies["refreshToken"];
            ////setTokenCookie(response.RefreshToken);
            return Ok(id);
        }
        [AllowAnonymous]
        [HttpPost("cpt")]
        public async Task<IActionResult> CreatePaymentToken(OBusiness_CPT_Request model)
        {
            var response = new Response<OBusiness_CPT_Response>();
            try
            {
                string amount = string.Empty;
                if (model.planId == 1)
                    amount = "20";
                else if(model.planId == 2)
                    amount = "20";
                else
                {
                    throw new Exception();
                }
                //model.password = Helper.EncodeTo64(model.password);
                //string RequestBody = $"{{\"amount\":{amount},\"udf\":{{\"seller_number\":{model.seller_number},\"snv\":\"{model.password}\",\"name\":\"{model.name}\", \"planId\":{model.planId},\"countryId\":{model.countryId}, \"phone_countryId\":\"{model.phone_countryId}\"}},\"contact_number\":\"{model.phone}\",\"email_id\":\"{model.email}\",\"currency\":\"INR\",\"mtx\":\"{Guid.NewGuid()}\"}}";
                string RequestBody = $"{{\"amount\":{amount},\"contact_number\":\"{model.phone}\",\"email_id\":\"{model.email}\",\"currency\":\"INR\",\"mtx\":\"{Guid.NewGuid()}\"}}";
                var client = new RestClient("https://sandbox-icp-api.bankopen.co/api/payment_token");
                var request = new RestRequest("https://sandbox-icp-api.bankopen.co/api/payment_token", Method.Post);
                request.AddHeader("accept", "application/json");
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer 72cbef60-719f-11ed-b807-45337cebb642:43783c58897b6f41a727b3095f672df63c96cd55");
                request.AddParameter("application/json", RequestBody, ParameterType.RequestBody);
                var open_response = client.Execute(request);
                if (open_response.StatusCode == HttpStatusCode.OK)
                {
                    var DeserializeResult = JsonConvert.DeserializeObject<OBusiness_CPT_Response>(open_response.Content);
                    //Helper.DecodeFrom64(DeserializeResult.udf.snv);

                    var hashPass = Helper.HashPasword(model.password, out var salt);
                    int agentSalesId = 0;
                    if (model.seller_number.HasValue && model.seller_number.Value > 0)
                    {
                        agentSalesId = model.seller_number.Value;
                    }
                    var user = new User
                    {
                        phone = model.phone,
                        hash_password = hashPass,
                        salt = salt,
                        agent_sales_userId = agentSalesId,
                        phone_countryId = model.phone_countryId,
                        name = model.name,
                        sales_planId = model.planId,
                        pt = DeserializeResult.id
                    };
                    var id = UserService.Register(user);
                    response.IsSuccess = true;
                    response.Item = DeserializeResult;
                }
                else if (open_response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var DeserializeResult = JsonConvert.DeserializeObject<OBusiness_CPT_Response>(open_response.Content);
                    response.IsSuccess = false;
                    response.Item = DeserializeResult;
                }
                else
                {
                    response.IsSuccess = false;
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = Helper.GetProperMessage(ex.Message, "User"); ;
                return BadRequest(response);
            }

        }
        //[AllowAnonymous]
        //[HttpPost("wh")]
        //public async Task<IActionResult> Webhook(OBusiness_Webhook model)
        //{

        //}
        //[AllowAnonymous]
        //[HttpPost("cpt")]
        //public async Task<IActionResult> CreatePaymentToken(OBusiness_CPT_Request model)
        //{
        //    string amount = string.Empty;
        //    if (model.PlanId == 1)
        //        amount = "20";
        //    else
        //    {
        //        amount = "200";
        //    }
        //    string request = $"{{\"amount\":{amount},\"udf\":{{\"seller_number\":{model.Seller_Number}}},\"contact_number\":\"{model.Phone}\",\"email_id\":\"{model.Email}\",\"currency\":\"INR\",\"mtx\":\"{21399921}\"}}";
        //    //"{\"amount\":2,\"udf\":{\"sellerId\":\"1\"},\"contact_number\":\"0791575835\",\"email_id\":\"mheidat@gmail.com\",\"currency\":\"INR\",\"mtx\":\"4241\"}"
        //    var client = new HttpClient();
        //    //"https://sandbox-icp-api.bankopen.co/api/payment_token"
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", "72cbef60-719f-11ed-b807-45337cebb642:43783c58897b6f41a727b3095f672df63c96cd55");
        //    var response = await client.PostAsJsonAsync("https://sandbox-icp-api.bankopen.co/api/payment_token", request);
        //    var cpt_result = await response.Content.ReadFromJsonAsync<OBusiness_CPT_Response>();
        //    return Ok(cpt_result);
        //    //var request = new RestRequest(Method.POST);
        //    //request.AddHeader("accept", "application/json");
        //    //request.AddHeader("content-type", "application/json");
        //    //request.AddHeader("Authorization", "Bearer 72cbef60-719f-11ed-b807-45337cebb642:43783c58897b6f41a727b3095f672df63c96cd55");
        //    //request.AddParameter("application/json", "{\"amount\":2,\"udf\":{\"sellerId\":\"1\"},\"contact_number\":\"0791575835\",\"email_id\":\"mheidat@gmail.com\",\"currency\":\"INR\",\"mtx\":\"4241\"}", ParameterType.RequestBody);
        //    //IRestResponse response = client.Execute(request);

        //    //"{\"amount\":100,\"contact_number\":\"0791575835\",\"email_id\":\"moha@gmail.com\",\"currency\":\"INR\",\"mtx\":\"222321\",\"udf\":{\"seller_number\":\"10\"}}"

        //    //respnse

        //    //{
        //    //      "amount": "22.00",
        //    //      "currency": "INR",
        //    //      "mtx": "12332",
        //    //      "attempts": 0,
        //    //      "sub_accounts_id": null,
        //    //      "id": "sb_pt_IWj5BvkhKbKXed",  ===> save it on db with user data
        //    //      "entity": "payment_token",
        //    //      "status": "created",
        //    //      "customer": {
        //    //                        "contact_number": "0791575835",
        //    //        "email_id": "mheidat@gmail.com",
        //    //        "id": "sb_cs_z2Lt5Y6Wn4gKN",
        //    //        "entity": "customer"
        //    //        }
        //    //}

        //}
        [AllowAnonymous]
        [HttpPost("cps")]
        public IActionResult CheckPaymentStatus(AuthenticateRequest model)
        {

            //var client = new RestClient("https://sandbox-icp-api.bankopen.co/api/payment_token/sb_pt_IWj5BvkhKbKXed/payment");
            //var request = new RestRequest(Method.GET);
            //request.AddHeader("accept", "application/json");
            //request.AddHeader("Authorization", "Bearer 72cbef60-719f-11ed-b807-45337cebb642:43783c58897b6f41a727b3095f672df63c96cd55");
            //IRestResponse response = client.Execute(request);

            //Response 
            //    204

            return Ok();
        }

        private void setTokenCookie(string token)
        {
            // append cookie with refresh token to the http response
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
        private string ipAddress()
        {
            // get source ip address for the current request
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public IActionResult RefreshToken()
        {

            try
            {
                //var refreshToken = Request.Cookies["refreshToken"];
                var appSourceId = HttpContext.Request.Headers["app_source_id"].FirstOrDefault();
                var rToken = HttpContext.Request.Headers["refreshToken"].FirstOrDefault();
                //var user = (User)HttpContext.Items["User"];
                var user = UserService.GetSalesUserAndRefreshTokensByToken(rToken);
                if (user == null) return BadRequest("invalid token");/* throw new KeyNotFoundException();*/
                if (user.user_statusId == (int)UserStatus.blocked) return BadRequest("3:Your account is blocked");
                else if(user.user_statusId == (int)UserStatus.inactive) return BadRequest("1:Your subscription has expired. Please reactivate your account to be able to use our services");
                //user = UserService.GetSalesUserAndRefreshTokensByUserId(user.id);
                var response = UserManager.RefreshToken(user, rToken, ipAddress(), appSourceId);
                setTokenCookie(response.RefreshToken);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("invalid token");
            }
        }


        [HttpPost("revoke-token")]
        public IActionResult RevokeToken(RevokeTokenRequest model)
        {
            // accept refresh token in request body or cookie
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });
            var user = (User)HttpContext.Items["User"];
            UserManager.RevokeToken(user.id, token, ipAddress());
            return Ok(new { message = "Token revoked" });
        }


        [HttpPost("logout")]
        public IActionResult ClearSession()
        {
            var user = (User)HttpContext.Items["User"];
            return Ok(UserService.RemoveRefreshTokenByUserId(user.id));
        }

        [HttpGet("refresh-tokens")]
        public IActionResult GetRefreshTokens()
        {
            var user = (User)HttpContext.Items["User"];
            user = UserManager.GetSalesUserAndRefreshTokensByUserId(user.id);
            return Ok(user.RefreshTokens);
        }














        //=================================
        [HttpPost("Webhock")]
        public ActionResult Webhockssa([FromBody] paymentData paymentData)
        {
            return Ok((User)HttpContext.Items["User"]);
        }
        //[HttpPost]
        //[Route("/UpdateSalesUser")]
        //public ActionResult Update([FromBody] User User)
        //{
        //    string otp = OTPGen.GenerateRandomOTP();
        //    var id = userService.update(User);
        //}
        [HttpPost]
        [Route("/RegisterSalesUser")]
        public ActionResult Add([FromBody] User User)
        {
            try
            {
                //generate hash otp
                string otp = OTPGen.GenerateRandomOTP();
                //HMACHasher hmac = new HMACHasher(otp);
                //User.otpHash = hmac.otpHash;
                User.otp = otp;

                //add user with the hashed otp
                var id = UserService.Add(User);

                // if usere has been added then send the otp to sales user
                if (Convert.ToInt32(id) > 0)
                {
                    try
                    {
                        //to-do implemnt send otp number
                    }
                    catch (Exception)
                    {

                        // user created but message not sent
                    }
                }
                return Ok(otp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("/Token")]
        public ActionResult Token(string otp, int countryId, string phone)
        {
            try
            {
                //var user = UserService.Get(phone, countryId, otp);
                var user = UserService.Get(phone, countryId);
                if (user != null)
                {
                    //should rename otp to otpHash on DB
                    //HMACHasher hmac = new HMACHasher(otp, user.otp /*user.otpHash*/);
                    if (user.otp == otp)
                    {
                        //to-do generate token 
                        user.token = GenerateToken(user);
                    }
                    else
                    {
                        throw new Exception("you are not authorized!");
                    }
                }
                return Ok(user.token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        private string GenerateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
             new Claim(ClaimTypes.MobilePhone, user.phone),
             new Claim(ClaimTypes.Name, user.name),
             new Claim(ClaimTypes.IsPersistent, user.name),
             new Claim(ClaimTypes.Country,user.phone_countryId.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                AppSettings.Secret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMonths(1)
                , signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(User request)
        {
            //if (user.phone != request.phone)
            //{
            //    return BadRequest("user not found");
            //}
            return Ok("my crazy token");
        }

        //[HttpPost(Name = "Add")]
        //public HttpResponseMessage Add(HttpRequestMessage request, User user)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        try
        //        {
        //            var response = userService.Add(user, dbTransaction);
        //            return request.CreateResponse(HttpStatusCode.OK, response); ;
        //        }
        //        catch (Exception ex)
        //        {
        //            return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
        //        }
        //    });
        //}

        //[HttpPost(Name = "Update")]
        //public HttpResponseMessage Update(HttpRequestMessage request, User user)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        try
        //        {
        //            var response = userService.Update(user, dbTransaction);
        //            return request.CreateResponse(HttpStatusCode.OK, response); ;
        //        }
        //        catch (Exception ex)
        //        {
        //            return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
        //        }
        //    });
        //}

        //[HttpPost(Name = "Delete")]
        //public HttpResponseMessage Delete(HttpRequestMessage request, Int32 userID)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        try
        //        {
        //            userService.Delete(userID, dbTransaction);
        //            return request.CreateResponse(HttpStatusCode.OK); ;
        //        }
        //        catch (Exception ex)
        //        {
        //            return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
        //        }
        //    });
        //}

        //[HttpGet(Name = "GetPagedData")]
        //public HttpResponseMessage GetPagedData(HttpRequestMessage request, Int32 pageFirstRow, Int32 pageRowCount, String toPageOn, String toSortOn)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        try
        //        {
        //            var response = userService.GetPagedData(pageFirstRow, pageRowCount, toPageOn, toSortOn);
        //            return request.CreateResponse(HttpStatusCode.OK, response); ;
        //        }
        //        catch (Exception ex)
        //        {
        //            return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
        //        }
        //    });
        //}

        //[HttpGet(Name = "Get")]
        //public HttpResponseMessage Get(HttpRequestMessage request, Int32 userID)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        try
        //        {
        //            var response = userService.Get(userID);
        //            return request.CreateResponse(HttpStatusCode.OK, response); ;
        //        }
        //        catch (Exception ex)
        //        {
        //            return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
        //        }
        //    });
        //}

        //[HttpGet(Name = "CountAll")]
        //public HttpResponseMessage CountAll(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        try
        //        {
        //            var response = userService.CountAll();
        //            return request.CreateResponse(HttpStatusCode.OK, response); ;
        //        }
        //        catch (Exception ex)
        //        {
        //            return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
        //        }
        //    });
        //}

        //[HttpGet(Name = "GetAll")]
        //public HttpResponseMessage GetAll(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        try
        //        {
        //            var response = userService.GetAll();
        //            return request.CreateResponse(HttpStatusCode.OK, response); ;
        //        }
        //        catch (Exception ex)
        //        {
        //            return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
        //        }
        //    });
        //}

        //[HttpGet(Name = "GetByPk")]
        //public HttpResponseMessage GetByPk(HttpRequestMessage request, Int32 userID)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        try
        //        {
        //            var response = userService.GetByPk(userID);
        //            return request.CreateResponse(HttpStatusCode.OK, response); ;
        //        }
        //        catch (Exception ex)
        //        {
        //            return request.CreateResponse(HttpStatusCode.InternalServerError, ex);
        //        }
        //    });
        //}

    }
}