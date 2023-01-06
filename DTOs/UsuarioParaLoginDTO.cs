using System.ComponentModel.DataAnnotations;

namespace LojaDeRoupasAPI.DTOs
{
    public class UsuarioParaLoginDTO
    {
        [Required(ErrorMessage = "Esse campo é obrigatório!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório!")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
