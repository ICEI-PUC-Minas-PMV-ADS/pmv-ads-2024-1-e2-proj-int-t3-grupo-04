using NextMidiaWeb.Domain.Persistence;

namespace NextMidiaWeb.Domain.Entities
{
    public class MidiaService
    {
        private readonly MidiaDbContext _context;

        public MidiaService(MidiaDbContext context)
        {
            _context = context;
        }

        public Midia FindById(long id)
        {
            return _context.midia.SingleOrDefault(d => d.Id == id);
        }

        public Midia FindByNome(string nome)
        {
            return _context.midia.SingleOrDefault(d => d.Nome == nome && d.IsDeleted == false);
        }

        public List<Midia> FindAll() { return _context.midia.Where(d => !d.IsDeleted).ToList(); }

        public void Create(Midia midia)
        {
            _context.midia.Add(midia);
            _context.SaveChanges();
        }

        public void Update(Midia midia)
        {
            _context.midia.Update(midia);
            _context.SaveChanges();
        }

        public void Delete(Midia midia)
        {
            midia.Delete();
            _context.SaveChanges();
        }
    }
}
