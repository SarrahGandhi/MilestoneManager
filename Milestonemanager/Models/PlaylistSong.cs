using System.ComponentModel.DataAnnotations;
namespace Milestonemanager.Models
{
    public class PlaylistSong
    {
        [Key]
        public int PlaylistSongID { get; set; }
        public int Order { get; set; }
        public int PlaylistID { get; set; }
        public Playlist Playlist { get; set; }
        public int SongID { get; set; }
        public Song Song { get; set; }
    }
    public class PlaylistSongDTO
    {
        public int PlaylistSongID { get; set; }
        public int Order { get; set; }
        public int PlaylistID { get; set; }
        public int SongID { get; set; }
    }
}