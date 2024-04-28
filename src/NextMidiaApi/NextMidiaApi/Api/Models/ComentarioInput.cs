using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace NextMidiaApi.Api.Models
{
    public class ComentarioInput
    {
        [Required]
        public string Texto { get; set; }
        [Required]
        public BigInteger Nota { get; set; }

    }
}
