using LojaDeRoupasAPI.Context;
using LojaDeRoupasAPI.Models;
using LojaDeRoupasAPI.Services.Intefaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LojaDeRoupasAPI.Services
{
    public class AuthService: IAuthService
    {
        private readonly LojaContext _context;

        public AuthService(LojaContext context) => _context = context;

        public async Task<string> CriarTokenAsync(string email)
        {
            Usuario usuario = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(user => user.Email.Equals(email));
            var chave = Encoding.ASCII.GetBytes(Settings.SigningKey);
            var gerenciadorToken = new JwtSecurityTokenHandler();
            var descritorToken = new SecurityTokenDescriptor()
            {
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, usuario.Nivel.ToString())
                }),
            };
            var token = gerenciadorToken.CreateToken(descritorToken);
            return gerenciadorToken.WriteToken(token);
        }


    }
}
