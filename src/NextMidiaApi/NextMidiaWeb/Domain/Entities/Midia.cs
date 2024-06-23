using NextMidiaWeb.Models.Input;
using System.ComponentModel.DataAnnotations.Schema;

namespace NextMidiaWeb.Domain.Entities
{
    public class Midia
    {
        #region Properties
        public int Id { get; set; }
        [NotMapped]
        public string Nome { get; set; }
        [NotMapped]
        public string Sinopse { get; set; }
        [NotMapped]
        public DateTime DataLancamento { get; set; }
        [NotMapped]
        public string ImagemCapa { get; set; }
        [NotMapped]
        public string ImagemPoster { get; set; }
        [NotMapped]
        public Categoria Categoria { get; set; }
        [NotMapped]
        public List<MidiaTag> Tags { get; set; }
        [NotMapped]
        public List<string> IdsGenero { get; set; }
        [NotMapped]
        public string LinguaOrigem { get; set; }
        [NotMapped]
        public int ContagemDeVotos { get; set; }
        [NotMapped]
        public decimal MediaDeVotos { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }
        [NotMapped]
        public string? Status { get; set; }
        [NotMapped]
        public long? Verba { get; set; }
        [NotMapped]
        public long? Bilheteria { get; set; }
        [NotMapped]
        public string? PaginaMidia { get; set; }
        [NotMapped]
        public List<Genero>? Generos { get; set; }
        [NotMapped]
        public List<Produtora>? Produtoras { get; set; }
        [NotMapped]
        public string? Trailer { get; set; }
        #endregion

        #region Constructors
        public Midia()
        {
                
        }

        public Midia(int id)
        {
                
        }
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
