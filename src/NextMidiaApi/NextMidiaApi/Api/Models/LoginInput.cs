using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace NextMidiaApi.Api.Models
{
    public class LoginInput
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }

    }
}
