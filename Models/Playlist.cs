namespace MusicCatalog.Models
{
    public class Playlist
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<int> SongsIDs { get; set; }

        public Playlist() {
            Id = 0;    
            Title = string.Empty;
            SongsIDs = new List<int>();
        }
    }
}
