using System.Diagnostics.Contracts;

namespace NextMidiaWeb.Models.ReponseObjects
{
    public class TMDBTrailersListObject : ITMDBReponseObject
    {
        public int id { get; set; }
        public List<TMDBTrailersObject> results { get; set; } = new List<TMDBTrailersObject>();
        ReponseType ITMDBReponseObject.type { get { return ReponseType.List; } set => throw new NotImplementedException(); }
    }

    public class TMDBTrailersObject()
    {
        public string name { get; set; }
        public string key { get; set;}
        public string site { get; set; }
        public string type { get; set; }
        public bool official { get; set; }
    }
}
