using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace NextMidiaWeb.Models.Input
{
    public class ComentarioInput
    {
        [Required]
        public int IdMidia { get; set; }
        
        [Required]
        public string Texto { get; set; }
        
        [Required]
        public int Nota { get; set; }   
    }
}
