using System.Numerics;

namespace NextMidiaApi.Domain.Entities
{
    public class MidiaFavoritada
    {
        public Guid IdMidia { get; set; }
        public Guid IdUsuario { get; set; }
        public DateTime DataFavorito { get; set; }
    }
}
