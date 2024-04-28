using NextMidiaApi.Api.Models;

namespace NextMidiaApi.Domain.Entities
{
    public class Categoria
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public bool IsDeleted { get; set; }

        public void Update(CategoriaInput input)
        {
            Nome = input.Nome;
        }

        public void Delete()
        {
            IsDeleted = true;
        }

    }
}
