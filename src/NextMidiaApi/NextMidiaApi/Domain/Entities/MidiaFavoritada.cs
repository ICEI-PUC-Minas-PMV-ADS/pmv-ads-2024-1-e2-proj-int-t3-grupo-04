using System.Numerics;

namespace NextMidiaApi.Domain.Entities
{
    public class MidiaFavoritada
    {
        public long IdMidia { get; set; }
        public long IdUsuario { get; set; }
        public DateTime DataFavorito { get; set; }
    }
}
