using System.Linq;

namespace MusicCatalog.Models
{
    public class Catalog
    {
        public List<Artist> Artists { get; set; }
        public List<Album> Albums { get; set; }
        public List<Playlist> Playlists { get; set; }
        public List<Song> Songs { get; set; }

        public Catalog()
        {
            Artists = new List<Artist>();
            Albums = new List<Album>();
            Playlists = new List<Playlist>();
            Songs = new List<Song>();
        }

        // Поиск артистов по имени
        public List<Artist> SearchArtists(string title)
        {
            return Artists.Where(artist => artist.Title.ToLower().Contains(title.ToLower())).ToList();
        }

        // Поиск альбомов и сборников по названию
        public List<Album> SearchAlbums(string title)
        {
            var albums = Albums.Where(album => album.Title.ToLower().Contains(title.ToLower())).ToList();
            return albums.Distinct().ToList();
        }

        // Поиск песен по названию, альбому и жанру
        public List<Song> SearchSongs(string title)
        {
            return Songs.Where(song =>
                song.Title.ToLower().Contains(title.ToLower()) 
            ).ToList();
        }

        public List<Song> SearchSongsByGenre(string title)
        {
            return Songs.Where(song =>
                song.Genres.Any(genre => genre.Title.ToLower().Contains(title.ToLower()))
            ).ToList();
        }

        public List<Song> SearchSongsByArtistName(string title)
        {
            var artist = Artists.FirstOrDefault(a => a.Title.ToLower().Contains(title.ToLower()));
            if (artist == null)
            {
                return new List<Song>();
            }
            return Songs.Where(song =>
                song.ArtistID == artist.Id
            ).ToList();
        }
    }
}
