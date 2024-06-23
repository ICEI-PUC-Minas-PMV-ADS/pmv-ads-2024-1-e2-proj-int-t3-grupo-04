namespace NextMidiaWeb.Models.ReponseObjects
{
    public class TMDBMediaDetailObject : ITMDBReponseObject
    {
        public string? status { get; set; }
        public long? budget { get; set; }
        public long? revenue { get; set; }
        public string? homepage { get; set; }
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public string poster_path { get; set; }
        public List<string> genre_ids { get; set; }
        public int id { get; set; }
        public string original_language { get; set; }
        public string overview { get; set; }
        public DateTime release_date { get; set; }
        public string title { get; set; }
        public bool video { get; set; }
        public decimal vote_average { get; set; }
        public int vote_count { get; set; }
        public ReponseType type { get { return ReponseType.Object; } }
        public List<genre>? genres { get; set; }
        public List<production_companie>? production_companies { get; set; }
        ReponseType ITMDBReponseObject.type { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    public class genre()
    {
        public string name { get; set; }
        public int id { get; set; }
    }

    public class production_companie()
    {
        public int id { get; set; }
        public string logo_path { get; set; }
        public string name { get; set; }
        public string origin_country { get; set; }
    }
}
