namespace NextMidiaWeb.Models.ReponseObjects
{
    public class TMDBMediaListReponseObject  : ITMDBReponseObject
    {
        public int page { get; set; }
        public List<TMDBMediaDetailObject> results { get; set; } = new List<TMDBMediaDetailObject>();
        public long total_pages { get; set; }
        public long total_results { get; set; }        
        ReponseType ITMDBReponseObject.type { get { return ReponseType.List; } set => throw new NotImplementedException(); }
    }   
}
