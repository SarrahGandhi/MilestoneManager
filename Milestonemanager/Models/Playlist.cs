using System.ComponentModel.DataAnnotations;

namespace Milestonemanager.Models
{
    public class Playlist
    {
        [Key]
        public int PlaylistID { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public ICollection<PlaylistSong> PlaylistSongs { get; set; } = new List<PlaylistSong>();
    }
    public class PlaylistDTO
    {
        public int PlaylistID { get; set; }

        public string Name { get; set; }
        public string CreatedBy { get; set; }
    }
}