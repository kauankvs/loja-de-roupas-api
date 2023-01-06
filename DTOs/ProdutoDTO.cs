using LojaDeRoupasAPI.Enums;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LojaDeRoupasAPI.DTOs
{
    public class ProdutoDTO
    {

        [Required(ErrorMessage = "Esse campo é obrigatório!")]
        [DataType(DataType.ImageUrl)]
        public string Imagem { get; set; }

        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório!")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório!")]
        [DataType(DataType.Currency)]
        public double Preco { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório!")]
        public Tipo Tipo { get; set; }
    }
}
