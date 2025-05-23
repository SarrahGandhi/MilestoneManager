﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Milestonemanager.Data;

#nullable disable

namespace Milestonemanager.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250402152248_WeddingplaylistCMS")]
    partial class WeddingplaylistCMS
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Milestonemanager.Models.Admin", b =>
                {
                    b.Property<int>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("AdminId"));

                    b.Property<int>("AdminCategory")
                        .HasColumnType("int");

                    b.Property<string>("AdminEmail")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("AdminName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("AdminPhone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.HasKey("AdminId");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("Milestonemanager.Models.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("EventId"));

                    b.Property<int>("EventCategory")
                        .HasColumnType("int");

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("EventLocation")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("EventId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Milestonemanager.Models.EventGuest", b =>
                {
                    b.Property<int>("GuestEventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("GuestEventId"));

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("EventKids")
                        .HasColumnType("int");

                    b.Property<int>("EventMen")
                        .HasColumnType("int");

                    b.Property<int>("EventWomen")
                        .HasColumnType("int");

                    b.Property<int>("GuestId")
                        .HasColumnType("int");

                    b.Property<bool>("IsRSVPAccepted")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("GuestEventId");

                    b.HasIndex("EventId");

                    b.HasIndex("GuestId");

                    b.ToTable("EventGuests");
                });

            modelBuilder.Entity("Milestonemanager.Models.EventSong", b =>
                {
                    b.Property<int>("EventSongID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("EventSongID"));

                    b.Property<int>("EventID")
                        .HasColumnType("int");

                    b.Property<int>("SongID")
                        .HasColumnType("int");

                    b.HasKey("EventSongID");

                    b.HasIndex("EventID");

                    b.HasIndex("SongID");

                    b.ToTable("EventSongs");
                });

            modelBuilder.Entity("Milestonemanager.Models.EventTask", b =>
                {
                    b.Property<int>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("TaskId"));

                    b.Property<int>("AdminId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("EventTaskCategory")
                        .HasColumnType("int");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("TaskDescription")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("TaskName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("TaskId");

                    b.HasIndex("AdminId");

                    b.HasIndex("EventId");

                    b.ToTable("EventTasks");
                });

            modelBuilder.Entity("Milestonemanager.Models.Guest", b =>
                {
                    b.Property<int>("GuestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("GuestId"));

                    b.Property<string>("GuestAddress")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("GuestCategory")
                        .HasColumnType("int");

                    b.Property<string>("GuestLocation")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("GuestName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("GuestNotes")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("GuestPhone")
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<bool>("IsInvited")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("GuestId");

                    b.ToTable("Guests");
                });

            modelBuilder.Entity("Milestonemanager.Models.GuestSongRequest", b =>
                {
                    b.Property<int>("RequestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("RequestID"));

                    b.Property<int>("EventID")
                        .HasColumnType("int");

                    b.Property<int>("GuestID")
                        .HasColumnType("int");

                    b.Property<int>("SongID")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("RequestID");

                    b.HasIndex("EventID");

                    b.HasIndex("GuestID");

                    b.HasIndex("SongID");

                    b.ToTable("GuestSongRequests");
                });

            modelBuilder.Entity("Milestonemanager.Models.Playlist", b =>
                {
                    b.Property<int>("PlaylistID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("PlaylistID"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("PlaylistID");

                    b.ToTable("Playlists");
                });

            modelBuilder.Entity("Milestonemanager.Models.PlaylistSong", b =>
                {
                    b.Property<int>("PlaylistSongID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("PlaylistSongID"));

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int>("PlaylistID")
                        .HasColumnType("int");

                    b.Property<int>("SongID")
                        .HasColumnType("int");

                    b.HasKey("PlaylistSongID");

                    b.HasIndex("PlaylistID");

                    b.HasIndex("SongID");

                    b.ToTable("PlaylistSongs");
                });

            modelBuilder.Entity("Milestonemanager.Models.Song", b =>
                {
                    b.Property<int>("SongID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("SongID"));

                    b.Property<string>("Artist")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("SongID");

                    b.ToTable("Songs");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Milestonemanager.Models.EventGuest", b =>
                {
                    b.HasOne("Milestonemanager.Models.Event", null)
                        .WithMany("EventGuests")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Milestonemanager.Models.Guest", null)
                        .WithMany("EventGuests")
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Milestonemanager.Models.EventSong", b =>
                {
                    b.HasOne("Milestonemanager.Models.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Milestonemanager.Models.Song", "Song")
                        .WithMany("EventSongs")
                        .HasForeignKey("SongID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Song");
                });

            modelBuilder.Entity("Milestonemanager.Models.EventTask", b =>
                {
                    b.HasOne("Milestonemanager.Models.Admin", null)
                        .WithMany("EventTasks")
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Milestonemanager.Models.Event", null)
                        .WithMany("EventTasks")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Milestonemanager.Models.GuestSongRequest", b =>
                {
                    b.HasOne("Milestonemanager.Models.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Milestonemanager.Models.Guest", "Guest")
                        .WithMany()
                        .HasForeignKey("GuestID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Milestonemanager.Models.Song", "Song")
                        .WithMany("GuestSongRequests")
                        .HasForeignKey("SongID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Guest");

                    b.Navigation("Song");
                });

            modelBuilder.Entity("Milestonemanager.Models.PlaylistSong", b =>
                {
                    b.HasOne("Milestonemanager.Models.Playlist", "Playlist")
                        .WithMany("PlaylistSongs")
                        .HasForeignKey("PlaylistID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Milestonemanager.Models.Song", "Song")
                        .WithMany("PlaylistSongs")
                        .HasForeignKey("SongID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Playlist");

                    b.Navigation("Song");
                });

            modelBuilder.Entity("Milestonemanager.Models.Admin", b =>
                {
                    b.Navigation("EventTasks");
                });

            modelBuilder.Entity("Milestonemanager.Models.Event", b =>
                {
                    b.Navigation("EventGuests");

                    b.Navigation("EventTasks");
                });

            modelBuilder.Entity("Milestonemanager.Models.Guest", b =>
                {
                    b.Navigation("EventGuests");
                });

            modelBuilder.Entity("Milestonemanager.Models.Playlist", b =>
                {
                    b.Navigation("PlaylistSongs");
                });

            modelBuilder.Entity("Milestonemanager.Models.Song", b =>
                {
                    b.Navigation("EventSongs");

                    b.Navigation("GuestSongRequests");

                    b.Navigation("PlaylistSongs");
                });
#pragma warning restore 612, 618
        }
    }
}
