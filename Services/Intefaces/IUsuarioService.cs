using LojaDeRoupasAPI.Controllers;
using LojaDeRoupasAPI.DTOs;
using LojaDeRoupasAPI.Enums;
using LojaDeRoupasAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LojaDeRoupasAPI.Services.Intefaces
{
    public interface IUsuarioService
    {
        public Task<ActionResult<Usuario>> CriarContaAsync(UsuarioDTO usuarioInput);
        public Task<ActionResult> FazerLoginAsync(UsuarioParaLoginDTO usuario);
        public Task<ActionResult> DeletarUsuarioAsync(string email, string senha);
        public Task<ActionResult> MudarEmailDaContaAsync(string emailAtual, string emailNovo);
        public Task<ActionResult> MudarSenhaDaContaAsync(string email, string senhaAtual, string senhaNova);
        public Task<ActionResult<UsuarioDisplayDTO>> DisplayMeuUsuarioAsync(string email);
        public Task<ActionResult<List<UsuarioDisplayDTO>>> DisplayUsuariosAsync();
        public Task<ActionResult> SubirUsuarioParaNivelDoisAsync(string chave, string email);
        public Task<ActionResult> SubirUsuarioParaNivelTresAsync(string chave, string email);
    }
}
