using NextMidiaWeb.Api.Models;
using System.Numerics;

namespace NextMidiaWeb.Domain.Entities
{
    public class Comentario
    {
        public Guid Id { get; set; }
        public Usuario Usuario { get; set; }
        public Midia Midia { get; set; }
        public string Texto { get; set; }
        public DateTime Data { get; set; }
        public long Nota { get; set; }
        public bool IsDeleted { get; set; }

        public void Update(ComentarioInput input)
        {
            Texto = input.Texto;
            Nota = input.Nota;
        }

        public void Delete()
        {
            IsDeleted = true;
        }

    }
}
