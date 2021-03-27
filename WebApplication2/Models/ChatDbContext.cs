using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplication2.Models
{
    public partial class ChatDbContext : DbContext
    {
        public ChatDbContext()
        {
        }

        public ChatDbContext(DbContextOptions<ChatDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Chat> Chat { get; set; }
        public virtual DbSet<ChatUsers> ChatUsers { get; set; }
        public virtual DbSet<Messages> Message { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Data Source=DESKTOP-5BSAPSF;Initial Catalog=ChatDb;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(entity => {
                entity.HasKey(x => x.UserId);
            });
            modelBuilder.Entity<Chat>(entity =>
            {
                entity.Property(e => e.ChatName).HasMaxLength(500);
            });

            modelBuilder.Entity<ChatUsers>(entity =>
            {
                entity.HasKey(e => e.ChatUserid);
            });

            modelBuilder.Entity<Messages>(entity =>
            {
                entity.HasKey(x => x.MessageId);
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Message).HasColumnName("Message");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
