using BombRush.Models;
using Microsoft.EntityFrameworkCore;

namespace BombRush.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomSettings> RoomSettings { get; set; }
        public DbSet<RoomPlayers> RoomPlayers { get; set; }
        public DbSet<MessageHistory> MessageHistories { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Room>()
                .HasOne(r => r.RoomSettings)
                .WithOne(rs => rs.Room)
                .HasForeignKey<RoomSettings>(rs => rs.Id);

            modelBuilder.Entity<RoomPlayers>()
                .HasOne(r => r.Room)
                .WithMany(r => r.RoomPlayers)
                .HasForeignKey(r => r.RoomId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MessageHistory>()
                .HasOne(mh => mh.Sender)
                .WithMany(s => s.MessageHistory)
                .HasForeignKey(mh => mh.SenderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MessageHistory>()
                .HasOne(mh => mh.Room)
                .WithMany(s => s.MessageHistory)
                .HasForeignKey(mh => mh.RoomId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
