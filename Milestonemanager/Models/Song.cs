using System.ComponentModel.DataAnnotations;
namespace Milestonemanager.Models
{
    public class Song
    {
        [Key]
        public int SongID { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }

        public ICollection<EventSong> EventSongs { get; set; } = new List<EventSong>();
        public ICollection<PlaylistSong> PlaylistSongs { get; set; } = new List<PlaylistSong>();
        public ICollection<GuestSongRequest> GuestSongRequests { get; set; } = new List<GuestSongRequest>();
    }
    public class SongDTO
    {
        public int SongID { get; set; }

        public string Title { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
    }
}

