using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace NextMidiaApi.Api.Models
{
    public class TagInput
    {
        [Required]
        public string Nome { get; set; }

    }
}
