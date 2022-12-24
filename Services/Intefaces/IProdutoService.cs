using LojaDeRoupasAPI.DTOs;
using LojaDeRoupasAPI.Enums;
using LojaDeRoupasAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LojaDeRoupasAPI.Services.Intefaces
{
    public interface IProdutoService
    {
        public Task<ActionResult<Produto>> AdicionarProdutoAsync(ProdutoDTO produtoInput);
        public Task<ActionResult> DeletarProdutoAsync(int id);
        public Task<ActionResult<Produto>> SelecionarProdutoAsync(int id);
        public Task<ActionResult<List<Produto>>> SelecionarTodosProdutosAsync();
        public Task<ActionResult<List<Produto>>> SelecionarProdutosPorMarcaAsync(string marca);
        public Task<ActionResult<List<Produto>>> SelecionarProdutosPorTipoAsync(Tipo tipo);
        public Task<ActionResult> AlterarPrecoDeProduto(int id, double preco);
    }
}
