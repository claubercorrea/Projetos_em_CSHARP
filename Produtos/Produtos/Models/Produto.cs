using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Produtos.Models
{
    public class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Isso diz ao banco que é autoincremento

        public int Id { get; set; }
        [Required(ErrorMessage = "O nome do produto e obrigatorio")]
        public string NomeProduto { get; set; }
        [Required(ErrorMessage ="SO números inteiros")]
        public int validade { get; set; }
        [Required(ErrorMessage = " Nome  da empresa obrigatorio")]
        public string Empresa { get; set; }

        [ForeignKey("Email")] // Especifica que a propriedade Email é a chave estrangeira
        public string  Email {get; set;} // Para associar o produto ao usuário que o cadastrou
}
}
