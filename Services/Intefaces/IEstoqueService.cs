using LojaDeRoupasAPI.DTOs;
using LojaDeRoupasAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LojaDeRoupasAPI.Services.Intefaces
{
    public interface IEstoqueService
    {
        public Task<ActionResult<Estoque>> AdicionarEstoqueTamanhoTParaProdutoAsync(EstoqueDTO estoqueInput);
        public Task<ActionResult> DeletarEstoqueTamanhoTDeProdutoAsync(int id);
    }
}
