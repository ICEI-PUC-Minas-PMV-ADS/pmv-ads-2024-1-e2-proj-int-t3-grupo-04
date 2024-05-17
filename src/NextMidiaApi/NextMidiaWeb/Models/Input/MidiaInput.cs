using NextMidiaWeb.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace NextMidiaWeb.Models.Input
{
    public class MidiaInput
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Sinopse { get; set; }
        [Required]
        public Categoria Categoria { get; set; }

    }
}
