using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace NextMidiaApi.Api.Models
{
    public class CategoriaInput
    {
        [Required]
        public string Nome { get; set; }

    }
}
