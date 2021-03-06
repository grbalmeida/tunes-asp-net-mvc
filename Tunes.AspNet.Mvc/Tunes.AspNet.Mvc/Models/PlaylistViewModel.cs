﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tunes.AspNet.Mvc.Models
{
    public class PlaylistViewModel
    {
        [Key]
        public int PlaylistId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(120, ErrorMessage = "O campo {0} deve possuir no máximo {1} caracteres")]
        public string Nome { get; set; }

        public IList<PlaylistFaixaViewModel> Faixas { get; set; }
    }
}