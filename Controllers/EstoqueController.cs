using LojaDeRoupasAPI.DTOs;
using LojaDeRoupasAPI.Enums;
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
        public async Task<ActionResult<Estoque>> AdicionarEstoqueTamanhoTParaProdutoAsync(EstoqueDTO estoqueInput) 
            => await _service.AdicionarEstoqueTamanhoTParaProdutoAsync(estoqueInput);

        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "Tres")]
        public async Task<ActionResult> DeletarEstoqueTamanhoTDeProdutoAsync(int id)
            => await _service.DeletarEstoqueTamanhoTDeProdutoAsync(id);

        [HttpGet]
        [Route("todos")]
        [AllowAnonymous]
        public async Task<ActionResult<Estoque>> SelecionarEstoqueDeTamanhoTDeProdutoAsync(int? estoqueId, Tamanho? tamanho, int? produtoId) 
            => await _service.SelecionarEstoqueDeTamanhoTDeProdutoAsync(estoqueId, tamanho, produtoId);

        [HttpGet]
        [Route("{produtoId}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Estoque>>> SelecionarTodosOsEstoquesDeProdutoAsync(int produtoId)
            => await _service.SelecionarTodosOsEstoquesDeProdutoAsync(produtoId);

        [HttpPut]
        [Route("quantidade/adicionar")]
        [Authorize(Roles = "Tres")]
        public async Task<ActionResult<Estoque>> AdicionarAoEstoqueDeTamanhoTDeProdutoAsync(int? estoqueId, Tamanho? tamanho, int? produtoId, int quantidadeAMais)
            => await _service.AdicionarAoEstoqueDeTamanhoTDeProdutoAsync(estoqueId, tamanho, produtoId, quantidadeAMais);

        [HttpPut]
        [Route("quantidade/subtrair")]
        [Authorize(Roles = "Tres")]
        public async Task<ActionResult<Estoque>> SubtrairDoEstoqueDeTamanhoTDeProdutoAsync(int? estoqueId, Tamanho? tamanho, int? produtoId, int quantidadeAMenos)
            => await _service.SubtrairDoEstoqueDeTamanhoTDeProdutoAsync(estoqueId, tamanho, produtoId, quantidadeAMenos);
    }
}
