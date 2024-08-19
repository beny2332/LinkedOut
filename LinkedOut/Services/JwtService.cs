using LinkedOut.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LinkedOut.Services
{
    public class JwtService
    { 
        private IConfiguration config;
        public JwtService(IConfiguration _config) 
        {
            config = _config;
        }
        public string genJWToken(UserModel user) 
        {
            // get key and exp from the config
            string? key = config.GetValue("JWT:key", string.Empty);
            int? exp = config.GetValue("JWT:exp", 3);

            // create byte key
            SymmetricSecurityKey secKey = new(Encoding.UTF8.GetBytes(key));

            // create credentials
            SigningCredentials crd = new(secKey, SecurityAlgorithms.HmacSha256);

            // create token payload (Claim)
            Claim[] claims = new[]
            {
                new Claim("id", user.id.ToString()),
                new Claim("username", user.userName),
            };

            // gen token object from all the data
            JwtSecurityToken token = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes((double)exp),
                signingCredentials: crd,
                claims: claims
                );

            // get token string(write token)
            string tkn = new JwtSecurityTokenHandler().WriteToken(token);
            // return the token string
            return tkn;
        }
    }
}
