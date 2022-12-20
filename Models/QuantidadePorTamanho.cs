﻿using LojaDeRoupasAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace LojaDeRoupasAPI.Models
{
    public class QuantidadePorTamanho
    {
        [Key]
        public int QuantidadePorTamanhoId { get; set; }
        public Tamanho Tamanho { get; set; }
        public int Quantidade { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
    }
}
