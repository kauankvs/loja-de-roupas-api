using LojaDeRoupasAPI.Context;
using LojaDeRoupasAPI.Controllers;
using LojaDeRoupasAPI.DTOs;
using LojaDeRoupasAPI.Enums;
using LojaDeRoupasAPI.Models;
using LojaDeRoupasAPI.Services.Intefaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LojaDeRoupasAPI.Services
{
    public class ProdutoService: IProdutoService
    {
        private readonly LojaContext _context;
        public ProdutoService(LojaContext context) => _context = context;
        
        public async Task<ActionResult<Produto>> AdicionarProdutoAsync(ProdutoDTO produtoInput) 
        {
            Produto produto = new Produto()
            {
                Imagem = produtoInput.Imagem,
                Descricao = produtoInput.Descricao,
                Marca = produtoInput.Marca,
                Preco = produtoInput.Preco,
                Tipos = produtoInput.Tipos,
            };
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
            return new CreatedResult(nameof(ProdutoController), produto);
        }

        public async Task<ActionResult> DeletarProdutoAsync(int id) 
        {
            Produto? produto = await _context.Produtos.FirstOrDefaultAsync(produto => produto.ProdutoId.Equals(id));
            if (produto.Equals(null))
                return new BadRequestResult();

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return new AcceptedResult();
        }

        public async Task<ActionResult<Produto>> SelecionarProdutoAsync(int id)
        {
            Produto? produto = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(produto => produto.ProdutoId.Equals(id));
            if (produto.Equals(null))
                return new NoContentResult();

            return new OkObjectResult(produto);
        }

        public async Task<ActionResult<List<Produto>>> SelecionarTodosProdutosAsync() 
        {
            List<Produto>? produtos = await _context.Produtos.AsNoTracking().ToListAsync();
            if (produtos.Equals(null))
                return new NoContentResult();

            return new OkObjectResult(produtos);
        }

        public async Task<ActionResult<List<Produto>>> SelecionarProdutosPorMarcaAsync(string marca)
        {
            List<Produto>? produtos = await _context.Produtos.AsNoTracking().Where(produto => produto.Marca.Equals(marca)).ToListAsync();
            if (produtos.Equals(null))
                return new NoContentResult();

            return new OkObjectResult(produtos);
        }

        public async Task<ActionResult<List<Produto>>> SelecionarProdutosPorTipoAsync(Tipo tipo)
        {
            List<Produto>? produtos = await _context.Produtos.AsNoTracking().Where(produto => produto.Tipos.Contains(tipo)).ToListAsync();
            if (produtos.Equals(null))
                return new NoContentResult();

            return new OkObjectResult(produtos);
        }

        public async Task<ActionResult> MudarPrecoDeProduto(int id, double preco) 
        {
            Produto? produto = await _context.Produtos.FirstOrDefaultAsync(produto => produto.ProdutoId.Equals(id));
            if (produto.Equals(null))
                return new BadRequestResult();

            produto.Preco = preco;
            await _context.SaveChangesAsync();
            return new AcceptedResult();
        }
    }
}
