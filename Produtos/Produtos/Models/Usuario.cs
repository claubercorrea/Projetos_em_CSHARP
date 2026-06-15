using System.ComponentModel.DataAnnotations;

namespace Produtos.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome e obrigadorio")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O email e obrigatorio")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "A  senha e obrigatorio")]
        
        public string Senha { get; set; }
        

        public string? TokenRecuperacao { get; set; } // O "bilhete" de recuperação



    }
}
