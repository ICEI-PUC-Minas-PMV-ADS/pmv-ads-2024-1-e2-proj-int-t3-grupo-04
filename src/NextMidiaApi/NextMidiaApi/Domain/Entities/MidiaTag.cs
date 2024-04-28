using NextMidiaApi.Api.Models;
using System.Numerics;

namespace NextMidiaApi.Domain.Entities
{
    public class MidiaTag
    {
        public Midia Midia { get; set; }
        public Tag Tag { get; set; }
        public BigInteger Peso { get; set; }
    }
}
