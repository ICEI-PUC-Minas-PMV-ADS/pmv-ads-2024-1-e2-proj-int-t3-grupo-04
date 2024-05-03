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

        public Categoria FindById(int id)
        {
            return  _context.categoria.SingleOrDefault(d => d.Id == id);
        }

        public Categoria FindByNome(string nome)
        {
            return _context.categoria.SingleOrDefault(d => d.Nome == nome);
        }

        public List<Categoria> FindAll() { return _context.categoria.ToList(); }

        public void Create(Categoria categoria)
        {
            _context.categoria.Add(categoria);
            _context.SaveChanges();
        }

        public void Update(Categoria categoria)
        {
            _context.categoria.Update(categoria);
            _context.SaveChanges();
        }

        public void Delete(Categoria categoria)
        {            
            _context.SaveChanges();
        }
    }
}
