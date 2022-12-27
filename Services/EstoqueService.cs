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
    public class EstoqueService: IEstoqueService
    {
        private readonly LojaContext _context;
        public EstoqueService(LojaContext context) => _context = context;
        
        public async Task<ActionResult<Estoque>> AdicionarEstoqueTamanhoTParaProdutoAsync(EstoqueDTO estoqueInput) 
        {
            Produto? produto = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(produto => produto.ProdutoId.Equals(estoqueInput.ProdutoId));
            if (produto.Equals(null))
                return new BadRequestResult();

            if (produto.Estoque.Any(e => e.Tamanho.Equals(estoqueInput.Tamanho)))
                return new ConflictResult();

            Estoque estoque = new Estoque()
            {
                Tamanho = estoqueInput.Tamanho,
                Quantidade = estoqueInput.Quantidade,
                ProdutoId = estoqueInput.ProdutoId,
            };
            await _context.Estoques.AddAsync(estoque);
            await _context.SaveChangesAsync();
            return new CreatedResult(nameof(EstoqueController), estoque);
        }

        public async Task<ActionResult> DeletarEstoqueTamanhoTDeProdutoAsync(int id) 
        {
            Estoque? estoque = await _context.Estoques.FirstOrDefaultAsync(e => e.EstoqueId.Equals(id));
            if (estoque.Equals(null))
                return new BadRequestResult();

            _context.Estoques.Remove(estoque);
            await _context.SaveChangesAsync();
            return new AcceptedResult();
        }

        public async Task<ActionResult<Estoque>> SelecionarEstoqueDeTamanhoTDeProdutoAsync(int? estoqueId, Tamanho? tamanho, int? produtoId) 
        {
            if(estoqueId != null) 
            {
                Estoque? estoque = await _context.Estoques.AsNoTracking().FirstOrDefaultAsync(e => e.EstoqueId.Equals(estoqueId));
                if (estoque.Equals(null))
                    return new BadRequestResult();

                return new OkObjectResult(estoque);
            }

            if(tamanho != null && produtoId != null) 
            {
                Produto? produto = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(e => e.ProdutoId.Equals(produtoId));
                if (produto.Equals(null))
                    return new BadRequestResult();

                Estoque? estoque = produto.Estoque.FirstOrDefault(e => e.Tamanho.Equals(tamanho));
                if (estoque.Equals(null))
                    return new BadRequestResult();

                return new OkObjectResult(estoque);
            }
            return new BadRequestResult();
        }

        public async Task<ActionResult<Estoque>> AdicionarAoEstoqueDeTamanhoTDeProdutoAsync(int? estoqueId, Tamanho? tamanho, int? produtoId, int quantidadeAMais)
        {
            if (estoqueId != null)
            {
                Estoque? estoque = await _context.Estoques.FirstOrDefaultAsync(e => e.EstoqueId.Equals(estoqueId));
                if (estoque.Equals(null))
                    return new BadRequestResult();
                
                estoque.Quantidade = estoque.Quantidade + quantidadeAMais;
                await _context.SaveChangesAsync();
                return new AcceptedResult();
            }

            if (tamanho != null && produtoId != null)
            {
                Produto? produto = await _context.Produtos.FirstOrDefaultAsync(e => e.ProdutoId.Equals(produtoId));
                if (produto.Equals(null))
                    return new BadRequestResult();

                Estoque? estoque = produto.Estoque.FirstOrDefault(e => e.Tamanho.Equals(tamanho));
                if (estoque.Equals(null))
                {
                    Estoque novoEstoque = new Estoque()
                    {
                        Tamanho = tamanho.Value,
                        Quantidade = quantidadeAMais,
                        ProdutoId = produto.ProdutoId,
                    };
                    await _context.Estoques.AddAsync(novoEstoque);
                    await _context.SaveChangesAsync();
                    return new CreatedResult(nameof(EstoqueController), novoEstoque);
                }
                estoque.Quantidade = estoque.Quantidade + quantidadeAMais;
                await _context.SaveChangesAsync();
                return new AcceptedResult();
            }
            return new BadRequestResult();
        }

        public async Task<ActionResult<Estoque>> SubtrairDoEstoqueDeTamanhoTDeProdutoAsync(int? estoqueId, Tamanho? tamanho, int? produtoId, int quantidadeAMenos)
        {
            if (estoqueId != null)
            {
                Estoque? estoque = await _context.Estoques.FirstOrDefaultAsync(e => e.EstoqueId.Equals(estoqueId));
                if (estoque.Equals(null))
                    return new BadRequestResult();

                estoque.Quantidade = estoque.Quantidade - quantidadeAMenos;
                await _context.SaveChangesAsync();
                return new AcceptedResult();
            }

            if (tamanho != null && produtoId != null)
            {
                Produto? produto = await _context.Produtos.FirstOrDefaultAsync(e => e.ProdutoId.Equals(produtoId));
                if (produto.Equals(null))
                    return new BadRequestResult();

                Estoque? estoque = produto.Estoque.FirstOrDefault(e => e.Tamanho.Equals(tamanho));
                if (estoque.Equals(null))
                    return new BadRequestResult();

                estoque.Quantidade = estoque.Quantidade - quantidadeAMenos;
                await _context.SaveChangesAsync();
                return new AcceptedResult();
            }
            return new BadRequestResult();
        }

        public async Task<ActionResult<List<Estoque>>> SelecionarTodosOsEstoquesDeProdutoAsync(int produtoId) 
        {
            List<Estoque>? estoquesProduto = await _context.Estoques.Where(e => e.ProdutoId.Equals(produtoId)).ToListAsync();
            if (estoquesProduto.Equals(null))
                return new NoContentResult();

            return new OkObjectResult(estoquesProduto);
        }

    }
}
