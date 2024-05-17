using Microsoft.EntityFrameworkCore;

namespace NextMidiaWeb.Domain.Entities
{
    [Keyless]
    public class MidiaTag
    {
        private int peso;
        public int MidiaId { get; set; }
        public int TagId { get; set; }
        public Midia Midia { get; set; }
        public Tag Tag { get; set; }
        public int Peso { get => peso; set => peso = value; }
    }
}
