using Microsoft.AspNetCore.Mvc;
using MusicCatalog.Models;

namespace MusicCatalog.Controllers
{
    [ApiController]
    [Route("api/catalog/v1")]
    public class MusicCatalogController : ControllerBase
    {
        private readonly Catalog _musicCatalog;

        public MusicCatalogController(Catalog musicCatalog)
        {
            _musicCatalog = musicCatalog;
        }

        // GET: api/catalog/v1/artists
        [HttpGet("artists")]
        public ActionResult<IEnumerable<Artist>> GetArtists()
        {
            return _musicCatalog.Artists;
        }

        // GET: api/catalog/v1/albums
        [HttpGet("albums")]
        public ActionResult<IEnumerable<Album>> GetAlbums()
        {
            return _musicCatalog.Albums;
        }

        // GET: api/catalog/v1/songs
        [HttpGet("songs")]
        public ActionResult<IEnumerable<Song>> GetSongs()
        {
            return _musicCatalog.Songs;
        }

        // GET: api/catalog/v1/playlists
        [HttpGet("playlists")]
        public ActionResult<IEnumerable<Playlist>> GetPlaylists()
        {
            return _musicCatalog.Playlists;
        }

        // POST: api/catalog/v1/artists
        [HttpPost("artists")]
        public ActionResult<Artist> CreateArtist(Artist artist)
        {
            var _ = _musicCatalog.Artists.FirstOrDefault(a => a.Id == artist.Id);
            if (_ != null)
            {
                return BadRequest("already exists");
            }
            _musicCatalog.Artists.Add(artist);
            return CreatedAtAction(nameof(GetArtists), new { id = artist.Id }, artist);
        }

        // POST: api/catalog/albums
        [HttpPost("albums")]
        public ActionResult<Album> CreateAlbum(Album album)
        {
            var artist = _musicCatalog.Artists.FirstOrDefault(a => a.Id == album.ArtistID);
            if (artist == null)
            {
                return NotFound("artist with this id doesn't exist");
            }
            var _ = _musicCatalog.Albums.FirstOrDefault(a => a.Id == album.ArtistID);
            if (_ != null)
            {
                return BadRequest("already exists");
            }
            _musicCatalog.Albums.Add(album);
            artist.AlbumsIDs.Add(album.Id);
            _musicCatalog.Songs.AddRange(album.Songs.GetRange(0, album.Songs.Count));
            return CreatedAtAction(nameof(GetAlbums), new { id = album.Id }, album);
        }

        // POST: api/catalog/songs
        [HttpPost("songs")]
        public ActionResult<Song> CreateSong(Song song)
        {
            if (_musicCatalog.Artists.FirstOrDefault(a => a.Id == song.ArtistID) == null)
            {
                return BadRequest();
            }
            if (_musicCatalog.Albums.FirstOrDefault(a => a.Id == song.AlbumID) == null)
            {
                return BadRequest();
            }
            else
            {
                return BadRequest("The album of the song does not exist in the catalog.");
            }
        }

        // POST: api/catalog/playlists
        [HttpPost("playlists")]
        public ActionResult<Playlist> CreatePlaylist(Playlist playlist)
        {
            _musicCatalog.Playlists.Add(playlist);
            return CreatedAtAction(nameof(GetPlaylists), new { id = playlist.Id }, playlist);
        }

        // GET: api/catalog/search
        [HttpGet("search")]
        public ActionResult<IEnumerable<object>> Search(string source, string param, string title)
        {
            switch (source)
            {
                case "song":
                    // Search for songs by artist title
                    switch (param)
                    {
                        case "title":
                            return _musicCatalog.SearchSongs(title);
                        case "genre":
                            return _musicCatalog.SearchSongsByGenre(title);
                        case "artist":
                            return _musicCatalog.SearchSongsByArtistName(title);
                    }
                    return new List<Song>();
                case "artist":
                    // Search for artists by title
                    return _musicCatalog.SearchArtists(title);
                case "album":
                    // Search for albums by artist title
                    return _musicCatalog.SearchAlbums(title);
                default:
                    return BadRequest("Invalid source specified.");
            }
        }
    }
}
