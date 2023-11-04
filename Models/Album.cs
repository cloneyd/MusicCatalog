namespace MusicCatalog.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ArtistID { get; set; }
        public List<Song> Songs { get; set; }

        public Album()
        {
            Id = 0;
            Title = string.Empty;
            ArtistID = 0;
            Songs = new List<Song>();
        }
    }
}
