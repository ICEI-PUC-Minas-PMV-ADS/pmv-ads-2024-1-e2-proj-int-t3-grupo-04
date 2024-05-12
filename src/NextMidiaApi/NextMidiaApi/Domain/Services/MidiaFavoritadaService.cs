using NextMidiaApi.Domain.Entities;
using NextMidiaApi.Domain.Persistence;

namespace NextMidiaApi.Domain.Services
{
    public class MidiaFavoritadaService
    {
        private readonly MidiaFavoritadaDbContext _context;

        public void Create(MidiaFavoritada fav)
        {
            _context.midiaFavoritada.Add(fav);
            _context.SaveChanges();
        }

        public MidiaFavoritada GetById(Guid idMidia, Guid idUsuario)
        {
            return _context.midiaFavoritada                
                .SingleOrDefault(s => s.IdMidia == idMidia && s.IdUsuario == idUsuario);
        }
    }
}
