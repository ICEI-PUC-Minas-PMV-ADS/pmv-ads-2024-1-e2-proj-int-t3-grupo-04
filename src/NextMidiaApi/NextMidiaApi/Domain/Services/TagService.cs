using NextMidiaApi.Api.Models;
using NextMidiaApi.Domain.Persistence;

namespace NextMidiaApi.Domain.Entities
{
    public class TagService
    {
        private readonly TagDbContext _context;

        public TagService(TagDbContext context)
        {
            _context = context;
        }

        public Tag FindById(int id)
        {
            return  _context.tag.SingleOrDefault(d => d.Id == id);
        }

        public Tag FindByNome(string nome)
        {
            return _context.tag.SingleOrDefault(d => d.Nome == nome);
        }

        public List<Tag> FindAll() { return _context.tag.ToList(); }

        public void Create(Tag tag)
        {
            tag.Id = GetLastId() <= 0 ? 1 : GetLastId();
            _context.tag.Add(tag);
            _context.SaveChanges();
        }

        public void Update(Tag tag)
        {
            _context.tag.Update(tag);
            _context.SaveChanges();
        }

        public void Delete(Tag tag)
        {
            tag.Delete();
            _context.SaveChanges();
        }

        private long GetLastId()
        {
            return _context.tag.SingleOrDefault() != null ?
                _context.tag.OrderByDescending(t => t.Id).ToList()[0].Id + 1
                : 1;
        }
    }
}
