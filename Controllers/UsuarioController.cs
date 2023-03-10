using LojaDeRoupasAPI.DTOs;
using LojaDeRoupasAPI.Models;
using LojaDeRoupasAPI.Services.Intefaces;
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
        public async Task<ActionResult<string>> FazerLoginAsync([FromForm] UsuarioParaLoginDTO usuario)
            => await _service.FazerLoginAsync(usuario);

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<ActionResult<Usuario>> CriarContaAsync([FromForm] UsuarioDTO usuarioInput)
            => await _service.CriarContaAsync(usuarioInput);  
        
        [HttpDelete]
        [Route("delete")]
        [Authorize]
        public async Task<ActionResult> DeletarUsuarioAsync(string senha)
            => await _service.DeletarUsuarioAsync(User.FindFirstValue(ClaimTypes.Email), senha);

        [HttpPut]
        [Route("mudar/email")]
        [Authorize]
        public async Task<ActionResult<Usuario>> MudarEmailDaContaAsync(string emailNovo)
            => await _service.MudarEmailDaContaAsync(User.FindFirstValue(ClaimTypes.Email), emailNovo);

        [HttpPut]
        [Route("mudar/senha")]
        [Authorize]
        public async Task<ActionResult<Usuario>> MudarSenhaDaContaAsync(string senhaAtual, string senhaNova)
            => await _service.MudarSenhaDaContaAsync(User.FindFirstValue(ClaimTypes.Email), senhaAtual, senhaNova);

        [HttpPut]
        [Route("mudar/nivel-dois")]
        [Authorize]
        public async Task<ActionResult> SubirUsuarioParaNivelDoisAsync(string chave, string email)
            => await _service.SubirUsuarioParaNivelDoisAsync(chave, email);

        [HttpPut]
        [Route("mudar/nivel-tres")]
        [Authorize(Roles = "Dois, Tres")]
        public async Task<ActionResult> SubirUsuarioParaNivelTresAsync(string chave, string email)
            => await _service.SubirUsuarioParaNivelTresAsync(chave, email);

        [HttpGet]
        [Route("conta")]
        [Authorize]
        public async Task<ActionResult<UsuarioDisplayDTO>> DisplayMeuUsuarioAsync()
            => await _service.DisplayMeuUsuarioAsync(User.FindFirstValue(ClaimTypes.Email));

        [HttpGet]
        [Route("usuarios")]
        [Authorize(Roles = "Dois, Tres")]
        public async Task<ActionResult<List<UsuarioDisplayDTO>>> DisplayUsuariosAsync() 
            => await _service.DisplayUsuariosAsync();

    }
}
