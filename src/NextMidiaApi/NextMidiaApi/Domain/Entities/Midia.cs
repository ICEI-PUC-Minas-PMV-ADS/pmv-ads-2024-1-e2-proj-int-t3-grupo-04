using NextMidiaApi.Api.Models;

namespace NextMidiaApi.Domain.Entities
{
    public class Midia
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sinopse { get; set; }
        public Categoria Categoria { get; set; }
        public List<MidiaTag> Tags { get; set; }
        public bool IsDeleted { get; set; }

        public void Update(MidiaInput input)
        {
            Nome = input.Nome;
            Sinopse = input.Sinopse;
            Categoria = input.Categoria;
        }

        public void Delete()
        {
            IsDeleted = true;
        }

    }
}
