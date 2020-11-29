using System.ComponentModel.DataAnnotations;

namespace Tunes.AspNet.Mvc.Models
{
    public class ItemNotaFiscalViewModel
    {
        [Key]
        public int ItemNotaFiscalId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal PrecoUnitario { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Quantidade { get; set; }

        public NotaFiscalViewModel NotaFiscal { get; set; }
        public FaixaViewModel Faixa { get; set; }
    }
}