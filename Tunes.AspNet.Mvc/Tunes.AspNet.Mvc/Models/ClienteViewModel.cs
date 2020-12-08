using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tunes.AspNet.Mvc.Models
{
    public class ClienteViewModel
    {
        [Key]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(40, ErrorMessage = "O campo {0} deve possuir no máximo {1} caracteres")]
        [Display(Name = "Primeiro Nome")]
        public string PrimeiroNome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(40, ErrorMessage = "O campo {0} deve possuir no máximo {1} caracteres")]
        public string Sobrenome { get; set; }

        [StringLength(80, ErrorMessage = "O campo {0} deve possuir no máximo {1} caracteres")]
        public string Empresa { get; set; }

        [StringLength(70, ErrorMessage = "O campo {0} deve possuir no máximo {1} caracteres")]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [StringLength(40, ErrorMessage = "O campo {0} deve possuir no máximo {1} caracteres")]
        public string Cidade { get; set; }

        [StringLength(40, ErrorMessage = "O campo {0} deve possuir no máximo {1} caracteres")]
        public string Estado { get; set; }

        [StringLength(40, ErrorMessage = "O campo {0} deve possuir no máximo {1} caracteres")]
        [Display(Name = "País")]
        public string Pais { get; set; }

        [StringLength(10, ErrorMessage = "O campo {0} deve possuir no máximo {1} caracteres")]
        public string CEP { get; set; }

        [StringLength(24, ErrorMessage = "O campo {0} deve possuir no máximo {1} caracteres")]
        public string Fone { get; set; }

        [StringLength(24, ErrorMessage = "O campo {0} deve possuir no máximo {1} caracteres")]
        public string Fax { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(60, ErrorMessage = "O campo {0} deve possuir no máximo {1} caracteres")]
        [EmailAddress(ErrorMessage = "O campo {0} está em um formato inválido")]
        public string Email { get; set; }

        [Display(Name = "Suporte")]
        public int SuporteId { get; set; }
        public FuncionarioViewModel Suporte { get; set; }

        public IList<NotaFiscalViewModel> NotasFiscais { get; set; }
        public IList<FuncionarioViewModel> SuporteTecnico { get; set; }
    }
}