namespace NextMidiaWeb.Api.Models
{
    public class TMDBReponseObject
    {
        public int page { get; set; }
        public List<Result> results { get; set; } = new List<Result>();
        public long total_pages { get; set; }
        public long total_results { get; set; }
    }

    public class Result
    {
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public List<string> genre_ids { get; set; }
        public long id {  get; set; }
        public string original_language { get; set; }
        public string overview { get; set; }
        public DateTime release_date { get; set; }
        public string title { get; set; }
        public bool video { get; set; }
        public decimal vote_average { get; set; }
        public int vote_count { get; set; }                
    }
}
