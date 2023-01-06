using BCrypt.Net;
using LojaDeRoupasAPI.Context;
using LojaDeRoupasAPI.Models;
using LojaDeRoupasAPI.Services.Intefaces;
using Microsoft.EntityFrameworkCore;


namespace LojaDeRoupasAPI.Services
{
    public class UsuarioAuthService: IUsuarioAuthService
    {
        private readonly LojaContext _context;
        public UsuarioAuthService(LojaContext context) => _context = context;

        public async Task<bool> VerificarSeSenhaECorretaAsync(string senha, string email)
        {
            Usuario? usuario = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(user => user.Email.Equals(email));
            bool senhaECorreta = BCrypt.Net.BCrypt.Verify(senha, usuario.Hash);
            return senhaECorreta;
        }
        public async Task<bool> VerificarSeUsuarioExisteAsync(string email)
        {
            bool usuarioExiste = await _context.Usuarios.AnyAsync(user => user.Email == email);
            return usuarioExiste;
        }
    }
}
