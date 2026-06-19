using domain.Ports;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infraestructure.Security
{
    public class ServicoSeguranca : IServicoSeguranca
    {
        private readonly string _jwtSecret;

        public ServicoSeguranca(IConfiguration configuration)
        {
            _jwtSecret = configuration["JwtSettings:Secret"] ?? throw new ArgumentNullException("JWT Secret não configurada.");
        }

        public string HashSenha(string senha)
        {
            return BCrypt.Net.BCrypt.HashPassword(senha);
        }

        public bool VerificarSenha(string senha, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(senha, hash);
        }

        public string GerarToken(Guid negocioId, string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.NameIdentifier, negocioId.ToString()),
                new Claim(ClaimTypes.Email, email)
                
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
