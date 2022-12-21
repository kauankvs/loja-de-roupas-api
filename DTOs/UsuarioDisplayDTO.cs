using System.ComponentModel.DataAnnotations;

namespace LojaDeRoupasAPI.DTOs
{
    public class UsuarioDisplayDTO
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        [DataType(DataType.ImageUrl)]
        public string? Foto { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Nivel { get; set; }
        [DataType(DataType.Date)]
        public string DataDeCriacao { get; set; }

    }
}
