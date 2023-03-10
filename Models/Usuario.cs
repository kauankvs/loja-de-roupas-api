using LojaDeRoupasAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace LojaDeRoupasAPI.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string? Foto { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        public int Idade { get; set; }
        public NivelDeAutorizacao Nivel { get; set; }
        public DateTime DataDeCriacao { get; set; }
    }
}
