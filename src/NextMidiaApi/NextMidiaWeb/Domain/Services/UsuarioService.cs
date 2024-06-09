using NextMidiaWeb.Domain.Persistence;
using NextMidiaWeb.Domain.Services;

namespace NextMidiaWeb.Domain.Entities
{
    public class UsuarioService
    {
        #region Properties
        private readonly UsuarioDbContext _context;
        private readonly MidiaFavoritadaDbContext _contextMidiaFavorita;
        private readonly MidiaFavoritadaService serviceMidiaFavoritada;
        #endregion

        #region Methods
        public UsuarioService(UsuarioDbContext context)
        {
            _context = context;
        }

        public Usuario FindByEmailAndSenha(string email, string senha)
        {
            return _context.Usuario.SingleOrDefault(d => d.Email == email && d.Senha == senha);
        }

        public Usuario FindById(long id)
        {
            return _context.Usuario.SingleOrDefault(d => d.Id == id);
        }

        public Usuario FindByEmail(string email)
        {
            return _context.Usuario.SingleOrDefault(d => d.Email == email /*&& d.IsDeleted == false*/);
        }

        public List<Usuario> FindAll() { return _context.Usuario/*.Where(d => !d.IsDeleted)*/.ToList(); }

        public void Create(Usuario usuario)
        {
            var id = _context.Usuario.OrderByDescending(u => u.Id).First().Id;
            usuario.Id = id + 1;

            _context.Usuario.Add(usuario);
            _context.SaveChanges();
        }

        public void Update(Usuario usuario)
        {
            foreach (Midia md in usuario.MidiasFavoritas)
            {
                var jaExisteFavorito = serviceMidiaFavoritada.GetById(md.Id, usuario.Id) != null;

                if (!jaExisteFavorito)
                    serviceMidiaFavoritada.Create(new MidiaFavoritada { Midia_Id = (int)md.Id, Usuario_Id = usuario.Id });
                else
                    _contextMidiaFavorita.Remove(new MidiaFavoritada { Midia_Id = (int)md.Id, Usuario_Id = usuario.Id });
            }

            _context.Usuario.Update(usuario);

            _context.SaveChanges();
            _contextMidiaFavorita.SaveChanges();
        }

        public void Delete(Usuario usuario)
        {
            usuario.Delete();
            _context.SaveChanges();
        }
        #endregion
    }
}
