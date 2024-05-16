using NextMidiaWeb.Api.Models;
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
        public Categoria Categoria { get; set; }
        [NotMapped]
        public List<MidiaTag> Tags { get; set; }
        public string LinguaOrigem { get; set; }
        public int VoteCount { get; set; }
        public decimal VoteAverage { get; set; }
        public bool IsDeleted { get; set; }
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
}
