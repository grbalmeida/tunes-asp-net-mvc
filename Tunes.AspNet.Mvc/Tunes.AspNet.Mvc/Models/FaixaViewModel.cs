using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tunes.AspNet.Mvc.Models
{
    public class FaixaViewModel
    {
        [Key]
        public int FaixaId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} deve possuir no máximo {1} caracteres")]
        public string Nome { get; set; }

        [StringLength(220, ErrorMessage = "O campo {0} deve possuir no máximo {1} caracteres")]
        public string Compositor { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Milissegundos { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Bytes { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Preço Unitário")]
        public decimal PrecoUnitario { get; set; }

        [Display(Name = "Álbum")]
        public int AlbumId { get; set; }

        [Display(Name = "Tipo de Mídia")]
        public int TipoMidiaId { get; set; }
       
        [Display(Name = "Gênero")]
        public int GeneroId { get; set; }

        public AlbumViewModel Album { get; set; }
        public TipoMidiaViewModel TipoMidia { get; set; }
        public GeneroViewModel Genero { get; set; }
        public IList<PlaylistFaixaViewModel> Playlists { get; set; }
        public IList<ItemNotaFiscalViewModel> ItensNotaFiscal { get; set; }

        public IList<AlbumViewModel> Albuns { get; set; }
        public IList<TipoMidiaViewModel> TiposDeMidia { get; set; }
        public IList<GeneroViewModel> Generos { get; set; }
    }
}