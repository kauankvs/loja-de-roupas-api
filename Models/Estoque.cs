using LojaDeRoupasAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace LojaDeRoupasAPI.Models
{
    public class Estoque
    {
        [Key]
        public int EstoqueId { get; set; }
        public Tamanho Tamanho { get; set; }
        public int Quantidade { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
    }
}
