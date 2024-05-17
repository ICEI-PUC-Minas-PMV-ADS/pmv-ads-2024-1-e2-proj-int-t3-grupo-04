using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace NextMidiaWeb.Models.Input
{
    public class LoginInput
    {

        [Required]
        [EmailAddress]
        [MinLength(5)]
        public string Email { get; set; }

        [Required]
        [MinLength(5)]
        public string Senha { get; set; }

    }
}
