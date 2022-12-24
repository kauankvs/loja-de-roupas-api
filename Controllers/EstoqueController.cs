using LojaDeRoupasAPI.DTOs;
using LojaDeRoupasAPI.Models;
using LojaDeRoupasAPI.Services.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LojaDeRoupasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstoqueController : ControllerBase
    {
        private readonly IEstoqueService _service;
        public EstoqueController(IEstoqueService service) => _service = service;

        [HttpPost]
        [Route("adicionar")]
        [Authorize(Roles = "Dois, Tres")]
        public async Task<ActionResult<Estoque>> AdicionarEstoqueDeTamanhoParaProdutoAsync(EstoqueDTO estoqueInput) 
            => await _service.AdicionarEstoqueDeTamanhoParaProdutoAsync(estoqueInput);

        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "Tres")]
        public async Task<ActionResult> DeletarEstoqueTamanhoTDeProdutoAsync(int id)
            => await _service.DeletarEstoqueTamanhoTDeProdutoAsync(id);


    }
}
