using NextMidiaApi.Api.Models;

namespace NextMidiaApi.Domain.Entities
{
    public class Tag
    {
        public long Id { get; set; }
        public string Nome { get; set; }

        //public List<MidiaTag> midias { get; set; }
        //public bool IsDeleted { get; set; }

        public void Update(TagInput input)
        {
            Nome = input.Nome;
        }

        public void Delete()
        {            
        }

    }
}
