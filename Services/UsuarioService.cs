using LojaDeRoupasAPI.Context;
using LojaDeRoupasAPI.Controllers;
using LojaDeRoupasAPI.DTOs;
using LojaDeRoupasAPI.Enums;
using LojaDeRoupasAPI.Models;
using LojaDeRoupasAPI.Services.Intefaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System.Globalization;
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

        public async Task<ActionResult> DeletarUsuarioAsync(string email, string senha)
        {
            if (_usuarioAuth.VerificarSeSenhaECorretaAsync(senha, email).Equals(false))
                return new ConflictResult();

            Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(user => user.Email.Equals(email));
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<ActionResult> MudarEmailDaContaAsync(string emailAtual, string emailNovo) 
        {
            bool emailNovoEstaEmUso = await _usuarioAuth.VerificarSeUsuarioExisteAsync(emailNovo);
            if (emailNovoEstaEmUso.Equals(true))
                return new ConflictResult();

            Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(user => user.Email.Equals(emailAtual));
            usuario.Email = emailNovo;
            await _context.SaveChangesAsync();
            return new AcceptedResult();
        }

        public async Task<ActionResult> MudarSenhaDaContaAsync(string email, string senhaAtual, string senhaNova) 
        {
            byte[] hash, salt;
            bool senhaAtualECorreta = await _usuarioAuth.VerificarSeSenhaECorretaAsync(senhaAtual, email);
            if (senhaAtualECorreta.Equals(false))
                return new BadRequestResult();

            Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(user => user.Email.Equals(email));
            _auth.TransformarSenhaEmHashESalt(senhaNova, out hash, out salt);
            usuario.Hash = hash;
            usuario.Salt = salt;
            await _context.SaveChangesAsync();
            return new AcceptedResult();
        }

        public async Task<ActionResult<UsuarioDisplayDTO>> DisplayMeuUsuarioAsync(string email) 
        {
            Usuario usuario = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(user => user.Email.Equals(email));
            UsuarioDisplayDTO usuarioDisplay = new UsuarioDisplayDTO()
            {
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                Email = email,
                Foto = usuario.Foto,
                Nivel = usuario.Nivel.ToString(),
                DataDeCriacao = usuario.DataDeCriacao.ToString("dd/MM/yyyy"),
            };
            return new OkObjectResult(usuarioDisplay);
        }

        public async Task<ActionResult<List<UsuarioDisplayDTO>>> DisplayUsuariosAsync() 
        {
            List<Usuario> usuarios = await _context.Usuarios.ToListAsync();
            List<UsuarioDisplayDTO> usuariosDisplay = new List<UsuarioDisplayDTO>();

            foreach(Usuario usuario in usuarios) 
            {
                usuariosDisplay.Add(new UsuarioDisplayDTO() 
                {
                    Nome = usuario.Nome,
                    Sobrenome = usuario.Sobrenome,
                    Email = usuario.Email,
                    Foto = usuario.Foto,
                    Nivel = usuario.Nivel.ToString(),
                    DataDeCriacao = usuario.DataDeCriacao.ToString("dd/MM/yyyy"),
                });
            }
            return new OkObjectResult(usuariosDisplay);
        }

        public async Task<ActionResult> SubirUsuarioParaNivelDoisAsync(string chave, string email) 
        {
            if (chave != Settings.ChaveNivelDois)
                return new BadRequestResult();

            bool usuarioExiste = await _usuarioAuth.VerificarSeUsuarioExisteAsync(email);
            if (usuarioExiste.Equals(false))
                return new NotFoundResult();

            Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(user => user.Email.Equals(email));
            usuario.Nivel = NivelDeAutorizacao.Dois;
            await _context.SaveChangesAsync();
            return new AcceptedResult();
        }

        public async Task<ActionResult> SubirUsuarioParaNivelTresAsync(string chave, string email)
        {
            if (chave != Settings.ChaveNivelTres)
                return new BadRequestResult();

            bool usuarioExiste = await _usuarioAuth.VerificarSeUsuarioExisteAsync(email);
            if (usuarioExiste.Equals(false))
                return new NotFoundResult();

            Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(user => user.Email.Equals(email));
            usuario.Nivel = NivelDeAutorizacao.Tres;
            await _context.SaveChangesAsync();
            return new AcceptedResult();
        }
    }
}
