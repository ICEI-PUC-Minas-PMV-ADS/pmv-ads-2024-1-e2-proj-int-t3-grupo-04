using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using NextMidiaApi.Api.Models;
using NextMidiaApi.Domain.Persistence;
using NextMidiaApi.Domain.Services;

namespace NextMidiaApi.Domain.Entities
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
            return _context.Usuarios.SingleOrDefault(d => d.Email == email && d.Senha == senha);
        }

        public Usuario FindById(long id)
        {
            return _context.Usuarios.SingleOrDefault(d => d.Id == id);
        }

        public Usuario FindByEmail(string email)
        {
            return _context.Usuarios.SingleOrDefault(d => d.Email == email && d.IsDeleted == false);
        }

        public List<Usuario> FindAll() { return _context.Usuarios.Where(d => !d.IsDeleted).ToList(); }

        public void Create(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public void Update(Usuario usuario)
        {
            foreach (Midia md in usuario.MidiasFavoritas)
            {
                var jaExisteFavorito = serviceMidiaFavoritada.GetById(md.Id, usuario.Id) != null;

                if (!jaExisteFavorito)
                    serviceMidiaFavoritada.Create(new MidiaFavoritada { IdMidia = md.Id, IdUsuario = usuario.Id });
                else
                    _contextMidiaFavorita.Remove(new MidiaFavoritada { IdMidia = md.Id, IdUsuario = usuario.Id });
            }

            _context.Usuarios.Update(usuario);

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
