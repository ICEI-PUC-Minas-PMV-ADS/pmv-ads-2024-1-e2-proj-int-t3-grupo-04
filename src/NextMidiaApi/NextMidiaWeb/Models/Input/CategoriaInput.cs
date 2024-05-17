using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace NextMidiaWeb.Models.Input
{
    public class CategoriaInput
    {
        [Required]
        public string Nome { get; set; }

    }
}
