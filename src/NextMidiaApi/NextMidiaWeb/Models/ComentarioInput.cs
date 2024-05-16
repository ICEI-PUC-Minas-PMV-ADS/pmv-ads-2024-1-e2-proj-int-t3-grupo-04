using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace NextMidiaWeb.Api.Models
{
    public class ComentarioInput
    {
        [Required]
        public string Texto { get; set; }
        [Required]
        public long Nota { get; set; }

    }
}
