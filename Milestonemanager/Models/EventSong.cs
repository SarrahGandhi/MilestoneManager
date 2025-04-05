using System.ComponentModel.DataAnnotations;
namespace Milestonemanager.Models
{
    public class EventSong
    {
        [Key]
        public int EventSongID { get; set; }

        public int EventID { get; set; }
        public Event Event { get; set; }
        public int SongID { get; set; }
        public Song Song { get; set; }
    }
    public class EventSongDTO
    {
        public int EventSongID { get; set; }
        public int EventID { get; set; }
        public int SongID { get; set; }
    }
}