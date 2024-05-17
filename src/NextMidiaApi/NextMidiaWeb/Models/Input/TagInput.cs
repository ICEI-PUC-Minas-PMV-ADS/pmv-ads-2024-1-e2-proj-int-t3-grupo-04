using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace NextMidiaWeb.Models.Input
{
    public class TagInput
    {
        [Required]
        public string Nome { get; set; }

    }
}
