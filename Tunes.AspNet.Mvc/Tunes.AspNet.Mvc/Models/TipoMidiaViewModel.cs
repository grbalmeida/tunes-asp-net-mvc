﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tunes.AspNet.Mvc.Models
{
    public class TipoMidiaViewModel
    {
        [Key]
        public int TipoMidiaId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(120, ErrorMessage = "O campo {0} deve possuir no máximo {1} caracteres")]
        public string Nome { get; set; }

        public IList<FaixaViewModel> Faixas { get; set; }
    }
}