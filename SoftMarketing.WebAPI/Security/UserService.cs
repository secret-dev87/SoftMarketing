using Microsoft.Extensions.Options;
using SoftMarketing.Model;
using SoftMarketing.Model.SalesModels;
using SoftMarketing.Services.Sales;
using SoftMarketing.WebAPI.Helpers;

namespace SoftMarketing.WebAPI.Security
{
    public interface IUserManager
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress);
        AuthenticateResponse RefreshToken(User user, string token, string ipAddress, string appSourceId = "0");
        void RevokeToken(int id, string token, string ipAddress);
        IEnumerable<User> GetAll();
        User GetById(int id);
        User GetSalesUserAndRefreshTokensByUserId(int id);
        SyncedData Sync(User user, List<string> toBeSynced);
        int UpdateHubConnections(RefreshToken refreshToken);
    }
    public class UserManager : IUserManager
    {
        //private UserService UserService;
        private IJwtUtils _jwtUtils;
        //private readonly IConfiguration Configuration;
        private readonly AppSettings AppSettings;
        private IUserService UserService;

        public UserManager(
            IJwtUtils jwtUtils,
            IOptions<AppSettings> _AppSettings,
             IUserService _UserService)
        {
            UserService = _UserService;
            _jwtUtils = jwtUtils;
            AppSettings = _AppSettings.Value;
            //Configuration = _configuration;
        }

        public int UpdateHubConnections(RefreshToken refreshToken)
        {
            return UserService.UpdateRefreshToken(refreshToken);
        }
        public SyncedData Sync(User user, List<string> toBeSynced)
        {
            return UserService.Sync(user, toBeSynced);
        }
        public AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress)
        {
            var user = UserService.Get(model.Phone, model.CountryId);
            try
            {
                if (user == null || model.password == null || !Helper.VerifyPassword(model.password, user.hash_password, user.salt))
                    throw new Exception("Username or password is incorrect");
               
            }
            catch (Exception)
            {

                throw new Exception("Username or password is incorrect");
            }
            if (user.user_statusId == (int)UserStatus.blocked)
            {
                throw new Exception("3:Your account is blocked");
            }
            else if (user.user_statusId == (int)UserStatus.inactive)
            {
                throw new Exception("1:Your subscription has expired. Please reactivate your account to be able to use our services");
            }

            // authentication successful so generate jwt and refresh tokens

            var jwtToken = _jwtUtils.GenerateJwtToken(user, model.AppSourceId);
            var refreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
            refreshToken.UserId = user.id;
            refreshToken.AppSourceId = Convert.ToInt16(model.AppSourceId);
            //user.RefreshTokens.Add(refreshToken);
            UserService.AddRefreshToken(refreshToken);

            // remove old refresh tokens from user

            user.RefreshTokens = UserService.GetRefreshTokensByUserId(user.id);

            var oldTokens = user.RefreshTokens.Where(x => !x.IsActive &&
                x.Created.AddDays(AppSettings.RefreshTokenTTL) <= DateTime.UtcNow);

            foreach (var oldToken in oldTokens)
            {
                UserService.RemoveRefreshTokenByTokenId(oldToken.Id);
            }
            //removeOldRefreshTokens(user);

            //// save changes to db
            //UserService.UpdateRefreshToken(user);

            return new AuthenticateResponse(user, jwtToken, refreshToken.Token);
        }
        public User GetById(int id)
        {
            var user = UserService.GetById(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }
        public User GetSalesUserAndRefreshTokensByUserId(int id)
        {
            var user = UserService.GetSalesUserAndRefreshTokensByUserId(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }
        private void removeOldRefreshTokens(User user)
        {
            // remove old inactive refresh tokens from user based on TTL in app settings
            user.RefreshTokens.RemoveAll(x =>
                !x.IsActive &&
                x.Created.AddDays(AppSettings.RefreshTokenTTL) <= DateTime.UtcNow);
        }

        public AuthenticateResponse RefreshToken(User user, string token, string ipAddress, string appSourceId ="0")
        {

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (refreshToken.IsRevoked)
            {
                // revoke all descendant tokens in case this token has been compromised
                revokeDescendantRefreshTokens(refreshToken, user, ipAddress, $"Attempted reuse of revoked ancestor token: {token}");
            }

            if (!refreshToken.IsActive || refreshToken.AppSourceId != Convert.ToInt16(appSourceId))
                throw new AppException("Invalid token");

            // replace old refresh token with a new one (rotate token)
            var newRefreshToken = rotateRefreshToken(refreshToken, ipAddress);
            newRefreshToken.AppSourceId = Convert.ToInt16(appSourceId);
            UserService.UpdateRefreshToken(refreshToken);
            newRefreshToken.UserId = user.id;
            UserService.AddRefreshToken(newRefreshToken);

            // remove old refresh tokens from user
            //removeOldRefreshTokens(user);
            //user.RefreshTokens = UserServiceDAL.GetRefreshTokensByUserId(user.id);
            var oldTokens = user.RefreshTokens.Where(x => !x.IsActive &&
                x.Created.AddDays(AppSettings.RefreshTokenTTL) <= DateTime.UtcNow);
            foreach (var oldToken in oldTokens)
            {
                UserService.RemoveRefreshTokenByTokenId(oldToken.Id);
            }

            // save changes to db
            //UserServiceDAL.UpdateRefreshToken(user);

            // generate new jwt
            var jwtToken = _jwtUtils.GenerateJwtToken(user, appSourceId);

            return new AuthenticateResponse(user, jwtToken, newRefreshToken.Token);
        }
        private User getUserByRefreshToken(string token)
        {
            var user = UserService.GetByRefreshToken(token);

            if (user == null)
                throw new AppException("Invalid token");

            return user;
        }
        private User GetSalesUserAndRefreshTokensByToken(string token)
        {
            var user = UserService.GetSalesUserAndRefreshTokensByToken(token);

            if (user == null)
                throw new AppException("Invalid token");

            return user;
        }
        public void RevokeToken(int id, string token, string ipAddress)
        {
            var user = GetSalesUserAndRefreshTokensByUserId(id);
            if (user.RefreshTokens == null)
            {
                throw new AppException("token not exist");
            }
            var refreshToken = user.RefreshTokens.FirstOrDefault(x => x.Token == token);
            if (refreshToken != null && refreshToken.IsActive)
            {

                //if (!refreshToken.IsActive)
                //    throw new AppException("Invalid token");

                // revoke token and save
                //revokeRefreshToken(refreshToken, ipAddress, "RevWitNoReplac");
                //UserServiceDAL.UpdateRefreshToken(refreshToken);
                UserService.RevokeRefreshToken(user, refreshToken, ipAddress, "RevWitNoReplac");
            }

        }
        private void revokeDescendantRefreshTokens(RefreshToken refreshToken, User user, string ipAddress, string reason)
        {
            // recursively traverse the refresh token chain and ensure all descendants are revoked
            if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            {
                var childToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
                try
                {
                    if (childToken.IsActive)
                    {

                        revokeRefreshToken(childToken, ipAddress, reason);
                        UserService.UpdateRefreshToken(childToken);
                    }
                    else
                        revokeDescendantRefreshTokens(childToken, user, ipAddress, reason);
                }
                catch (Exception)
                {
                }
            }
        }

        private void revokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replacedByToken;
        }

        private RefreshToken rotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            var newRefreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
            revokeRefreshToken(refreshToken, ipAddress, "Rep by n t", newRefreshToken.Token);
            newRefreshToken.AppSourceId = refreshToken.AppSourceId;
            newRefreshToken.ConnectionId = refreshToken.ConnectionId;
            return newRefreshToken;
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
