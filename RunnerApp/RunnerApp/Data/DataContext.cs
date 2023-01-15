using Microsoft.EntityFrameworkCore;
using RunnerApp.Entities;

namespace RunnerApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Map> Maps { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Friend> Friends { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Message>()
                .HasOne(u => u.Recipient)
                .WithMany(m => m.MessagesReceived)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
               .HasOne(u => u.Sender)
               .WithMany(m => m.MessagesSent)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Friend>()
               .HasOne(u => u.AppUser)
               .WithMany(m => m.Followers)
               .HasForeignKey(u => u.AppUserId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Friend>()
               .HasOne(u => u.FriendUser)
               .WithMany(m => m.MyFriends)
               .HasForeignKey(u => u.FriendId)
               .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
