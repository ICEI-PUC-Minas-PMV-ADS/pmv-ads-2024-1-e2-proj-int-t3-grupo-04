using NextMidiaWeb.Models.Input;
using System.ComponentModel.DataAnnotations.Schema;

namespace NextMidiaWeb.Domain.Entities
{
    public class Midia
    {
        #region Properties
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Sinopse { get; set; }
        public DateTime DataLancamento { get; set; }
        public string ImagemCapa { get; set; }
        public string ImagemPoster { get; set; }
        public Categoria Categoria { get; set; }
        [NotMapped]
        public List<MidiaTag> Tags { get; set; }
        public List<string> IdsGenero { get; set; }
        public string LinguaOrigem { get; set; }
        public int ContagemDeVotos { get; set; }
        public decimal MediaDeVotos { get; set; }
        public bool IsDeleted { get; set; }        
        public string? Status { get; set; }
        public long? Verba { get; set; }
        public long? Bilheteria { get; set; }
        public string? PaginaMidia { get; set; }                
        public List<Genero>? Generos { get; set; }
        public List<Produtora>? Produtoras { get; set; }

        public string? Trailer { get; set; }
        #endregion

        #region Methods
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
        #endregion        
    }

    public class Genero()
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }

    public class Produtora()
    {
        public int Id { get; set; }
        public string Logo { get; set; }
        public string Nome { get; set; }
        public string PaisOrigem { get; set; }
    }
}
