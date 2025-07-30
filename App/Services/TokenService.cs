using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using App.Models;
using System.Text;
using dotenv.net;

namespace App.Service
{
    public static class TokenService
    {
        public static string GenerateToken(User user)
        {

            //para rodar sem docker localmente
            /*
            var envVars = DotEnv.Read();
            string SecretKey = envVars["SECRETKEY"];
            */
            
            string secretKey = Environment.GetEnvironmentVariable("SECRETKEY");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.Id.ToString())
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}