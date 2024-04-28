using NextMidiaApi.Api.Models;
using NextMidiaApi.Domain.Persistence;

namespace NextMidiaApi.Domain.Entities
{
    public class UsuarioService
    {
        private readonly UsuarioDbContext _context;

        public UsuarioService(UsuarioDbContext context)
        {
            _context = context;
        }

        public Usuario FindByEmailAndSenha(string email, string senha)
        {
            return _context.Usuarios.SingleOrDefault(d => d.Email == email && d.Senha == senha);
        }

        public Usuario FindById(Guid id)
        {
            return  _context.Usuarios.SingleOrDefault(d => d.Id == id);
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
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }

        public void Delete(Usuario usuario)
        {
            usuario.Delete();
            _context.SaveChanges();
        }
    }
}
