using SoftMarketing.Model.SalesModels;
using System.Text.Json.Serialization;

namespace SoftMarketing.WebAPI.Security
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string JwtToken { get; set; }

        //[JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }

        public AuthenticateResponse(User user, string jwtToken, string refreshToken)
        {
            Id = user.id;
            Name = user.name;
            Phone = user.phone;
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
        }
    }
}
