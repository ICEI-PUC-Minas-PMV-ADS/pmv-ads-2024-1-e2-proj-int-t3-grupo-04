using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace NextMidiaWeb.Models.Input
{
    public class UsuarioInput
    {
        [Required]
        [MinLength(5)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [MinLength(8)]        
        public string Email { get; set; }
        
        [Required]
        [MinLength(8)]
        public string Senha { get; set; }
        
        [Required]
        [MinLength(8)]
        public string ConfirmacaoSenha { get; set; }

    }
}
