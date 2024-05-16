using System.Numerics;

namespace NextMidiaWeb.Domain.Entities
{
    public class MidiaFavoritada
    {
        public long IdMidia { get; set; }
        public long IdUsuario { get; set; }
        public DateTime DataFavorito { get; set; }
    }
}
