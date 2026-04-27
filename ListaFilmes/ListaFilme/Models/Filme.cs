using System;
using System.ComponentModel.DataAnnotations;

namespace ListaFilme.Models
{
    public class Filme
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage= "O campo titulo e  obrigatorio")]
        public string Title { get; set; }
        [Required(ErrorMessage = "O campo genero e obrigatorio")]
        public string Genero { get; set; }
        [Required (ErrorMessage = "O campo duracao e obrigatorio")]
        [DisplayFormat(DataFormatString ="(hh:mm)")]
        public  DateTime Duracao { get; set; }
        [Required(ErrorMessage = "O campo diretor e obrigatorio")]
        [Display(Name = "Diretor")]
        public string Diretor { get; set; }     
        [Required(ErrorMessage = "O campo lancamento e obrigatorio")]
        [DataType(DataType.Date)]
        
        public DateTime Lancamento { get; set; }




    }
}
