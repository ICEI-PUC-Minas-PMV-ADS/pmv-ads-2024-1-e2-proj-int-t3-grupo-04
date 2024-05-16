using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace NextMidiaWeb.Api.Models
{
    public class TagInput
    {
        [Required]
        public string Nome { get; set; }

    }
}
