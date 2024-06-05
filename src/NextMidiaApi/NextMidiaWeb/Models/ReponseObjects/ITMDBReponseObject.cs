namespace NextMidiaWeb.Models.ReponseObjects
{
    public interface ITMDBReponseObject
    {
        public ReponseType type { get; set; }
    }

    public enum ReponseType
    {
        Object = 0,
        List = 1,
        Trailer = 2
    }
}
