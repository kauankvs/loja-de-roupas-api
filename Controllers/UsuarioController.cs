using LojaDeRoupasAPI.DTOs;
using LojaDeRoupasAPI.Models;
using LojaDeRoupasAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LojaDeRoupasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;
        public UsuarioController(IUsuarioService service) => _service = service;

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> FazerLoginAsync(string email, string senha) 
        {
            var token = await _service.FazerLoginAsync(email, senha);
            HttpContext.Session.SetString("Token", token.ToString());
            return new OkObjectResult(token);
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<ActionResult<Usuario>> CriarContaAsync(UsuarioDTO usuarioInput)
            => await _service.CriarContaAsync(usuarioInput);  
        
        [HttpDelete]
        [Route("delete")]
        [Authorize]
        public async Task<ActionResult<Usuario>> DeletarUsuarioAsync(string senha)
            => await _service.DeletarUsuarioAsync(User.FindFirstValue(ClaimTypes.Email), senha);
    }
}
