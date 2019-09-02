using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DAL.Models
{
    public partial class MyMayoContext : DbContext
    {
        public MyMayoContext()
        {
        }

        public MyMayoContext(DbContextOptions<MyMayoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<RoleMaster> RoleMaster { get; set; }
        public virtual DbSet<UserMaster> UserMaster { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=MyMayo;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleMaster>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<UserMaster>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.HasIndex(e => e.UserName)
                    .HasName("IX_UserMaster");

                entity.Property(e => e.UserId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Image).HasColumnType("image");

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Qualification).HasMaxLength(50);

                entity.Property(e => e.Rating).HasColumnType("decimal(2, 1)");

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserMaster)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_UserMaster_RoleMaster");
            });
        }
    }
}
