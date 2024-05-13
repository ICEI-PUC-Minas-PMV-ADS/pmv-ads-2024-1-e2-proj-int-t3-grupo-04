using NextMidiaApi.Api.Models;
using System.Numerics;

namespace NextMidiaApi.Domain.Entities
{
    public class MidiaTag
    {
        private int peso;
        public Midia Midia { get; set; }
        public Tag Tag { get; set; }
        public int Peso { get => peso; set => peso = value; }
    }
}
