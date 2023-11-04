namespace MusicCatalog.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ArtistID { get; set; }  
        public int AlbumID { get; set; }
        public List<Genre> Genres { get; set; }

        public Song()
        {
            Id = 0;
            Title = string.Empty;
            ArtistID = 0;
            AlbumID = 0;
            Genres = new List<Genre>();
        }
    }
}
