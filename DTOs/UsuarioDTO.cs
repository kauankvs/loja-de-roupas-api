using System.ComponentModel.DataAnnotations;

namespace LojaDeRoupasAPI.DTOs
{
    public class UsuarioDTO
    {
        [Required(ErrorMessage = "Esse campo é obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório!")]
        public string Sobrenome { get; set; }

        [DataType(DataType.ImageUrl)]
        public string? Foto { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório!")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório!")]
        public int Idade { get; set; }
    }
}
