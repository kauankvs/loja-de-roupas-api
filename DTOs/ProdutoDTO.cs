using LojaDeRoupasAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace LojaDeRoupasAPI.DTOs
{
    public class ProdutoDTO
    {

        [Required(ErrorMessage = "Esse campo é obrigatório!")]
        [DataType(DataType.ImageUrl)]
        public string Imagem { get; set; }
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório!")]
        [DataType(DataType.Currency)]
        public double Preco { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório!")]
        public Tipo[] Tipos { get; set; }
    }
}
