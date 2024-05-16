using NextMidiaWeb.Domain.Entities;
using NextMidiaWeb.Domain.Persistence;

namespace NextMidiaWeb.Domain.Services
{
    public class MidiaFavoritadaService
    {
        private readonly MidiaFavoritadaDbContext _context;

        public void Create(MidiaFavoritada fav)
        {
            _context.midiaFavoritada.Add(fav);
            _context.SaveChanges();
        }

        public MidiaFavoritada GetById(long idMidia, long idUsuario)
        {
            return _context.midiaFavoritada                
                .SingleOrDefault(s => s.IdMidia == idMidia && s.IdUsuario == idUsuario);
        }
    }
}
