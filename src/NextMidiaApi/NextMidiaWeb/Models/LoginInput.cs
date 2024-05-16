using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace NextMidiaWeb.Api.Models
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
