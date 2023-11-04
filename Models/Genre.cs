namespace MusicCatalog.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public Genre()
        {
            Id = 0;
            Title = string.Empty;
        }
    }
}
