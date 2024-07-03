//using Microsoft.Extensions.Options;
//using SoftMarketing.Model;
//using SoftMarketing.Service;
//using SoftMarketing.WebAPI.Helpers;

//namespace SoftMarketing.WebAPI.Security
//{
//    public interface IUsersManager
//    {
//        AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress);
//        AuthenticateResponse RefreshToken(string token, string ipAddress);
//        void RevokeToken(string token, string ipAddress);
//        IEnumerable<User> GetAll();
//        User GetById(int id);
//    }
//    public class UsersManager
//    {
//        private UserService UserService;
//        private IJwtUtils _jwtUtils;
//        //private readonly IConfiguration Configuration;
//        private readonly AppSettings AppSettings;

//        public UsersManager(
//            UserService _UserService,
//            IJwtUtils jwtUtils,
//            IOptions<AppSettings> _AppSettings)
//        {
//            UserService = _UserService;
//            _jwtUtils = jwtUtils;
//            AppSettings = _AppSettings.Value;
//            //Configuration = _configuration;
//        }
//        public AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress)
//        {
//            var user = UserService.Get(model.Phone, model.CountryId);

//            // validate
//            //if (user == null || !BCrypt.Verify(model.Password, user.PasswordHash))
//            //    throw new AppException("Username or password is incorrect");
//            if (user == null || model.OTP != user.otp)
//                throw new Exception("Username or otp is incorrect");

//            // authentication successful so generate jwt and refresh tokens
//            var jwtToken = _jwtUtils.GenerateJwtToken(user);
//            var refreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
//            refreshToken.UserId = user.id;
//            //user.RefreshTokens.Add(refreshToken);
//            UserService.AddRefreshToken(refreshToken);

//            // remove old refresh tokens from user

//            user.RefreshTokens = UserService.GetRefreshTokensByUserId(user.id);

//            var oldTokens = user.RefreshTokens.Where(x => !x.IsActive &&
//                x.Created.AddDays(AppSettings.RefreshTokenTTL) <= DateTime.UtcNow);

//            foreach(var oldToken in oldTokens)
//            {
//                UserService.RemoveRefreshToken(oldToken.Id);
//            }
//            //removeOldRefreshTokens(user);

//            //// save changes to db
//            //UserService.UpdateRefreshToken(user);

//            return new AuthenticateResponse(user, jwtToken, refreshToken.Token);
//        }
//        public User GetById(int id)
//        {
//            var user = UserService.GetById(id);
//            if (user == null) throw new KeyNotFoundException("User not found");
//            return user;
//        }
//        private void removeOldRefreshTokens(User user)
//        {
//            // remove old inactive refresh tokens from user based on TTL in app settings
//            user.RefreshTokens.RemoveAll(x =>
//                !x.IsActive &&
//                x.Created.AddDays(AppSettings.RefreshTokenTTL) <= DateTime.UtcNow);
//        }

//        public AuthenticateResponse RefreshToken(string token, string ipAddress)
//        {
//            var user = getUserByRefreshToken(token);
//            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

//            if (refreshToken.IsRevoked)
//            {
//                // revoke all descendant tokens in case this token has been compromised
//                revokeDescendantRefreshTokens(refreshToken, user, ipAddress, $"Attempted reuse of revoked ancestor token: {token}");
//                UserService.UpdateRefreshToken(user);
//            }

//            if (!refreshToken.IsActive)
//                throw new AppException("Invalid token");

//            // replace old refresh token with a new one (rotate token)
//            var newRefreshToken = rotateRefreshToken(refreshToken, ipAddress);
//            user.RefreshTokens.Add(newRefreshToken);

//            // remove old refresh tokens from user
//            removeOldRefreshTokens(user);

//            // save changes to db
//            UserService.UpdateRefreshToken(user);

//            // generate new jwt
//            var jwtToken = _jwtUtils.GenerateJwtToken(user);

//            return new AuthenticateResponse(user, jwtToken, newRefreshToken.Token);
//        }
//        private User getUserByRefreshToken(string token)
//        {
//            var user = UserService.GetByRefreshToken(token);

//            if (user == null)
//                throw new AppException("Invalid token");

//            return user;
//        }
//        public void RevokeToken(string token, string ipAddress)
//        {
//            var user = getUserByRefreshToken(token);
//            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

//            if (!refreshToken.IsActive)
//                throw new AppException("Invalid token");

//            // revoke token and save
//            revokeRefreshToken(refreshToken, ipAddress, "Revoked without replacement");
//            UserService.UpdateRefreshToken(user);
//        }
//        private void revokeDescendantRefreshTokens(RefreshToken refreshToken, User user, string ipAddress, string reason)
//        {
//            // recursively traverse the refresh token chain and ensure all descendants are revoked
//            if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
//            {
//                var childToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
//                if (childToken.IsActive)
//                    revokeRefreshToken(childToken, ipAddress, reason);
//                else
//                    revokeDescendantRefreshTokens(childToken, user, ipAddress, reason);
//            }
//        }

//        private void revokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
//        {
//            token.Revoked = DateTime.UtcNow;
//            token.RevokedByIp = ipAddress;
//            token.ReasonRevoked = reason;
//            token.ReplacedByToken = replacedByToken;
//        }

//        private RefreshToken rotateRefreshToken(RefreshToken refreshToken, string ipAddress)
//        {
//            var newRefreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
//            revokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);
//            return newRefreshToken;
//        }
//    }
//}
