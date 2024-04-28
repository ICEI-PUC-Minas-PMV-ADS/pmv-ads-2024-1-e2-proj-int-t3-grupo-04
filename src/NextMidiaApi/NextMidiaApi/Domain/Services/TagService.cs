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

        public Tag FindById(Guid id)
        {
            return  _context.tags.SingleOrDefault(d => d.Id == id);
        }

        public Tag FindByNome(string nome)
        {
            return _context.tags.SingleOrDefault(d => d.Nome == nome && d.IsDeleted == false);
        }

        public List<Tag> FindAll() { return _context.tags.Where(d => !d.IsDeleted).ToList(); }

        public void Create(Tag tag)
        {
            _context.tags.Add(tag);
            _context.SaveChanges();
        }

        public void Update(Tag tag)
        {
            _context.tags.Update(tag);
            _context.SaveChanges();
        }

        public void Delete(Tag tag)
        {
            tag.Delete();
            _context.SaveChanges();
        }
    }
}
