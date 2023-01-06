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
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _service;
        public ProdutoController(IProdutoService service) => _service = service;

        [HttpPost]
        [Route("adicionar")]
        [Authorize(Roles = "Dois, Tres")]
        public async Task<ActionResult<Produto>> AdicionarProdutoAsync(ProdutoDTO produtoInput) 
            => await _service.AdicionarProdutoAsync(produtoInput);

        [HttpDelete]
        [Route("deletar")]
        [Authorize(Roles = "Tres")]
        public async Task<ActionResult> DeletarProdutoAsync(int id) 
            => await _service.DeletarProdutoAsync(id);

        [HttpPut]
        [Route("alterar/preco")]
        [Authorize(Roles = "Tres")]
        public async Task<ActionResult> AlterarPrecoDeProduto(int id, double preco)
            => await _service.AlterarPrecoDeProduto(id, preco);

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Produto>> SelecionarProdutoAsync(int id) 
            => await _service.SelecionarProdutoAsync(id);

        [HttpGet]
        [Route("todos")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Produto>>> SelecionarTodosProdutosAsync() 
            => await _service.SelecionarTodosProdutosAsync();

        [HttpGet]
        [Route("{marca}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Produto>>> SelecionarProdutosPorMarcaAsync(string marca) 
            => await _service.SelecionarProdutosPorMarcaAsync(marca);

        [HttpGet]
        [Route("tipo/{tipo}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Produto>>> SelecionarProdutosPorTipoAsync(string tipo) 
            => await _service.SelecionarProdutosPorTipoAsync((Tipo)Enum.Parse(typeof(Tipo), tipo));

    }
}
