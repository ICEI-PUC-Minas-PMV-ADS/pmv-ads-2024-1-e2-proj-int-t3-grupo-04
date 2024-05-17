using NextMidiaWeb.Models.Input;

namespace NextMidiaWeb.Domain.Entities
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
