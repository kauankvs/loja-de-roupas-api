using LojaDeRoupasAPI.Controllers;
using LojaDeRoupasAPI.DTOs;
using LojaDeRoupasAPI.Enums;
using LojaDeRoupasAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LojaDeRoupasAPI.Services
{
    public interface IUsuarioService
    {
        public  Task<ActionResult<Usuario>> CriarContaAsync(UsuarioDTO usuarioInput);
        public Task<ActionResult<string>> FazerLoginAsync(string email, string senha);
        public Task<ActionResult<Usuario>> DeletarUsuarioAsync(string email, string senha);
    }
}
