using LojaDeRoupasAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace LojaDeRoupasAPI.Models
{
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }
        public string Imagem { get; set; }
        public string? Descricao { get; set; }
        public double Preco { get; set; }
        public Tipo[] Tipos { get; set; }
        public ICollection<QuantidadePorTamanho>? QuantidadePorTamanho { get; set; }
    }
}
