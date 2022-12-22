using LojaDeRoupasAPI.Controllers;
using LojaDeRoupasAPI.DTOs;
using LojaDeRoupasAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LojaDeRoupasAPI.Services.Intefaces
{
    public interface IProdutoService
    {
        public Task<ActionResult<Produto>> AdicionarProdutoAsync(ProdutoDTO produtoInput);
        public Task<ActionResult> DeletarProdutoAsync(int id);
        public Task<ActionResult<Produto>> SelecionarProdutoAsync(int id);
        public Task<ActionResult<List<Produto>>> SelecionarTodosProdutosAsync();
    }
}
