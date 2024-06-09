using System.Numerics;

namespace NextMidiaWeb.Domain.Entities
{
    public class MidiaFavoritada
    {
        public int Midia_Id { get; set; }
        public int Usuario_Id { get; set; }
        public DateTime Data { get; set; }
    }
}
