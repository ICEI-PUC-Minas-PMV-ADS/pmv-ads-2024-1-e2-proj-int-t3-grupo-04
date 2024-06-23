using NextMidiaWeb.Domain.Entities;

namespace NextMidiaWeb.Models.ViewModel
{
    public class TagViewModel
    {
        public string Tag { get; set; }
        public List<Midia> Midias { get; set; }
        public bool tagEspecifica { get; set; } = false;
    }
}
