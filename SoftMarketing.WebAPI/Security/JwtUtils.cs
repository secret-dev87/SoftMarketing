using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SoftMarketing.Model;
using SoftMarketing.Model.SalesModels;
using SoftMarketing.Services.Sales;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
namespace SoftMarketing.WebAPI.Security
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(User user, string appSourceId = "0");
        public AuthenticateRequest? ValidateJwtToken(string token);
        public RefreshToken GenerateRefreshToken(string ipAddress);
    }

    public class JwtUtils : IJwtUtils
    {
        private UserService UserService;
        private readonly AppSettings AppSettings;
        //private readonly IConfiguration _configuration;

        public JwtUtils(
        IOptions<AppSettings> _AppSettings)
        {
            UserService = new SoftMarketing.Services.Sales.UserService();
            AppSettings = _AppSettings.Value;
        }

        public string GenerateJwtToken(User user, string appSourceId = "0")
        {
            // generate token that is valid for 15 minutes
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                new Claim("id", user.id.ToString()),
                new Claim("phone", user.phone),
                new Claim("countryId", user.phone_countryId.ToString()),
                new Claim("app_source_id", appSourceId)
            }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public AuthenticateRequest? ValidateJwtToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettings.Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                var phone = (jwtToken.Claims.First(x => x.Type == "phone").Value);
                var countryId = int.Parse(jwtToken.Claims.First(x => x.Type == "countryId").Value);
                var appSourceId = Convert.ToString(jwtToken.Claims.First(x => x.Type == "app_source_id").Value);
                AuthenticateRequest authReq = new AuthenticateRequest
                {
                    UserId = userId,
                    CountryId = countryId,
                    Phone = phone,
                    AppSourceId = appSourceId
                };
                // return user id from JWT token if validation successful
                return authReq;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }

        public RefreshToken GenerateRefreshToken(string ipAddress)
        {
            var refreshToken = new RefreshToken
            {
                Token = getUniqueToken(),
                // token is valid for 7 days
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };

            return refreshToken;

            string getUniqueToken()
            {
                // token is a cryptographically strong random sequence of values
                var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
                // ensure token is unique by checking against db
                var tokenIsUnique = UserService.GetRTokenByToken(token);
                if (tokenIsUnique != null)
                {
                    return getUniqueToken();
                }
                return token;
            }
        }
    }
}