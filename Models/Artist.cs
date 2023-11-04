namespace MusicCatalog.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<int> AlbumsIDs { get; set; }

        public Artist() {
            Id = 0;
            Title = string.Empty;
            AlbumsIDs = new List<int>();
        }
    }
}
