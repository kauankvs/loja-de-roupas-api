using LojaDeRoupasAPI.Context;
using LojaDeRoupasAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace LojaDeRoupasAPI.Services
{
    public class UsuarioAuthService: IUsuarioAuthService
    {
        private readonly LojaContext _context;
        public UsuarioAuthService(LojaContext context) => _context = context;

        public async Task<bool> VerificarSeSenhaECorretaAsync(string senha, string email)
        {
            Usuario? usuario = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(user => user.Email.Equals(email));
            using (var hmac = new HMACSHA512(usuario.Salt))
            {
                var hashComputado = hmac.ComputeHash(Encoding.UTF8.GetBytes(senha));
                return hashComputado.Equals(usuario.Hash);
            }
        }
        public async Task<bool> VerificarSeUsuarioExisteAsync(string email)
        {
            bool usuarioExiste = await _context.Usuarios.AnyAsync(user => user.Email == email);
            return usuarioExiste;
        }
    }
}
