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
    public class UsuarioService
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
                return new NotFoundObjectResult(usuario);

            bool senheCorreta = await _auth.ChecarSeSenhaECorretaAsync(usuario.Senha, usuario.Email);
            if (senheCorreta.Equals(false))
                return new ForbidResult();

            var token = await _auth.CriarTokenAsync(usuario.Email);
            return token;
        }
    }
}
