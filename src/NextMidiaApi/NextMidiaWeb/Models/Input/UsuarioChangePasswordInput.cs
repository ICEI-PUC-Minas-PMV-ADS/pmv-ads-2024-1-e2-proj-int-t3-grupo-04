using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace NextMidiaWeb.Models.Input
{
    public class UsuarioChangePasswordInput
    {
        [Required]
        public string Senha { get; set; }
        [Required]
        public string ConfirmacaoSenha { get; set; }

    }
}
