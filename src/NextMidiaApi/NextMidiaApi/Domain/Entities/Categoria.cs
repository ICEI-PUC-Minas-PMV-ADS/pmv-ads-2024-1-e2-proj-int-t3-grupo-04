using NextMidiaApi.Api.Models;

namespace NextMidiaApi.Domain.Entities
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }        

        public void Update(CategoriaInput input)
        {
            Nome = input.Nome;
        }       
    }
}
