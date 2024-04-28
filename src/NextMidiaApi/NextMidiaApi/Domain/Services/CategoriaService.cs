using NextMidiaApi.Api.Models;
using NextMidiaApi.Domain.Persistence;

namespace NextMidiaApi.Domain.Entities
{
    public class CategoriaService
    {
        private readonly CategoriaDbContext _context;

        public CategoriaService(CategoriaDbContext context)
        {
            _context = context;
        }

        public Categoria FindById(Guid id)
        {
            return  _context.categorias.SingleOrDefault(d => d.Id == id);
        }

        public Categoria FindByNome(string nome)
        {
            return _context.categorias.SingleOrDefault(d => d.Nome == nome && d.IsDeleted == false);
        }

        public List<Categoria> FindAll() { return _context.categorias.Where(d => !d.IsDeleted).ToList(); }

        public void Create(Categoria categoria)
        {
            _context.categorias.Add(categoria);
            _context.SaveChanges();
        }

        public void Update(Categoria categoria)
        {
            _context.categorias.Update(categoria);
            _context.SaveChanges();
        }

        public void Delete(Categoria categoria)
        {
            categoria.Delete();
            _context.SaveChanges();
        }
    }
}
