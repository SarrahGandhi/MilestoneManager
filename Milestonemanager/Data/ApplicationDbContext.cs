using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Milestonemanager.Models;

namespace Milestonemanager.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Guest> Guests { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<EventGuest> EventGuests { get; set; }
    public DbSet<EventTask> EventTasks { get; set; }
    public DbSet<EventSong> EventSongs { get; set; }
    public DbSet<Playlist> Playlists { get; set; }
    public DbSet<PlaylistSong> PlaylistSongs { get; set; }
    public DbSet<Song> Songs { get; set; }
    public DbSet<GuestSongRequest> GuestSongRequests { get; set; }
}
