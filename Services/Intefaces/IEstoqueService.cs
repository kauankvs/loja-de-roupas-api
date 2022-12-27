using LojaDeRoupasAPI.DTOs;
using LojaDeRoupasAPI.Enums;
using LojaDeRoupasAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LojaDeRoupasAPI.Services.Intefaces
{
    public interface IEstoqueService
    {
        public Task<ActionResult<Estoque>> AdicionarEstoqueTamanhoTParaProdutoAsync(EstoqueDTO estoqueInput);
        public Task<ActionResult> DeletarEstoqueTamanhoTDeProdutoAsync(int id);
        public Task<ActionResult<Estoque>> SelecionarEstoqueDeTamanhoTDeProdutoAsync(int? estoqueId, Tamanho? tamanho, int? produtoId);
        public Task<ActionResult<Estoque>> AdicionarAoEstoqueDeTamanhoTDeProdutoAsync(int? estoqueId, Tamanho? tamanho, int? produtoId, int quantidadeAMais);
        public Task<ActionResult<Estoque>> SubtrairDoEstoqueDeTamanhoTDeProdutoAsync(int? estoqueId, Tamanho? tamanho, int? produtoId, int quantidadeAMenos);
        public Task<ActionResult<List<Estoque>>> SelecionarTodosOsEstoquesDeProdutoAsync(int produtoId);
    }
}
