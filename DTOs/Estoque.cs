using LojaDeRoupasAPI.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace LojaDeRoupasAPI.DTOs
{
    public class Estoque
    {
        [JsonProperty("tamanho")]
        [JsonConverter(typeof(StringEnumConverter))]
        [Required(ErrorMessage = "Esse campo é obrigatório!")]
        public Tamanho Tamanho { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório!")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório!")]
        public int ProdutoId { get; set; }
    }
}
