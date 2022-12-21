using LojaDeRoupasAPI.Context;
using LojaDeRoupasAPI.Controllers;
using LojaDeRoupasAPI.DTOs;
using LojaDeRoupasAPI.Enums;
using LojaDeRoupasAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace LojaDeRoupasAPI.Services
{
    public class UsuarioService: IUsuarioService
    {
        private readonly LojaContext _context;
        private readonly IAuthService _auth;
        private readonly IUsuarioAuthService _usuarioAuth;
        public UsuarioService(LojaContext context, IAuthService senha, IUsuarioAuthService usuarioAuth)
        {
            _context = context;
            _auth = senha;
            _usuarioAuth = usuarioAuth;
        }

        public async Task<ActionResult<Usuario>> CriarContaAsync(UsuarioDTO usuarioInput)
        {
            byte[] hash, salt;
            bool usuarioExiste = await _usuarioAuth.VerificarSeUsuarioExisteAsync(usuarioInput.Email);
            if (usuarioExiste.Equals(true))
                return new ConflictResult();

            _auth.TransformarSenhaEmHashESalt(usuarioInput.Senha, out hash, out salt);
            Usuario usuario = new Usuario()
            {
                Nome = usuarioInput.Nome,
                Sobrenome = usuarioInput.Sobrenome,
                Foto = usuarioInput.Foto,
                Email = usuarioInput.Email,
                Hash = hash,
                Salt = salt,
                Nivel = NivelDeAutorizacao.Tres,
                DataDeCriacao = DateTime.Now,
            };

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return new CreatedResult(nameof(UsuarioController), usuario);
        }

        public async Task<ActionResult<string>> FazerLoginAsync(string email, string senha)
        {
            bool usuarioExiste = await _usuarioAuth.VerificarSeUsuarioExisteAsync(email);
            if (usuarioExiste.Equals(false))
                return new NotFoundResult();

            bool senheCorreta = await _usuarioAuth.VerificarSeSenhaECorretaAsync(senha, email);
            if (senheCorreta.Equals(false))
                return new ConflictResult();

            var token = await _auth.CriarTokenAsync(email);
            return token;
        }

        public async Task<ActionResult<Usuario>> DeletarUsuarioAsync(string email, string senha)
        {
            if (_usuarioAuth.VerificarSeSenhaECorretaAsync(senha, email).Equals(false))
                return new ConflictResult();

            Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(user => user.Email.Equals(email));
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return new OkResult();
        }
    }
}
