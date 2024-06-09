using System.Numerics;

namespace NextMidiaWeb.Domain.Entities
{
    public class MidiaFavoritada
    {
        public long Midia_Id { get; set; }
        public long Usuario_Id { get; set; }
        public DateTime Data { get; set; }
    }
}
