using NextMidiaWeb.Domain.Entities;

namespace NextMidiaWeb.Models.ViewModel
{
    public class DetalheMidiaViewModel
    {
        public Midia midia { get; set; }
        public bool mostrarComentarios { get; set; } = false;
        public List<ComentarioUsuario> comentariosUsuario { get; set; }
    }

    public class ComentarioUsuario
    {
        public string NomeUsuario { get; set; }
        public string Texto { get; set; }
        public DateTime Data { get; set; }
    }
}
