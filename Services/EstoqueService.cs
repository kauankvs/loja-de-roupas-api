using LojaDeRoupasAPI.Context;
using LojaDeRoupasAPI.Controllers;
using LojaDeRoupasAPI.DTOs;
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
    }
}
