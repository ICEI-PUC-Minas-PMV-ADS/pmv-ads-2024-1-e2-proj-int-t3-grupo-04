using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace NextMidiaWeb.Models.Input
{
    public class ComentarioInput
    {
        [Required]
        public string Texto { get; set; }
        [Required]
        public long Nota { get; set; }

    }
}
