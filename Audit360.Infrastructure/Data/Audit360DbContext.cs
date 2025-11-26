using Audit360.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Audit360.Infrastructure.Data
{
    public class Audit360DbContext : DbContext
    {
        public Audit360DbContext(DbContextOptions<Audit360DbContext> options) : base(options)
        {
        }

        public DbSet<Responsible> Responsibles { get; set; } = null!;
        public DbSet<Audit> Audits { get; set; } = null!;
        public DbSet<Finding> Findings { get; set; } = null!;
        public DbSet<FollowUp> FollowUps { get; set; } = null!;

        public DbSet<AuditStatus> AuditStatuses { get; set; } = null!;
        public DbSet<FindingType> FindingTypes { get; set; } = null!;
        public DbSet<FindingSeverity> FindingSeverities { get; set; } = null!;
        public DbSet<FollowUpStatus> FollowUpStatuses { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure many-to-many between User and Role with explicit join table name
            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<Dictionary<string, object>>("UserRole",
                    ur => ur.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                    ur => ur.HasOne<User>().WithMany().HasForeignKey("UserId")
                );

            // Optional: set max lengths and indexes
            modelBuilder.Entity<User>(b =>
            {
                b.HasIndex(u => u.Username).IsUnique();
                b.HasIndex(u => u.Email).IsUnique();
                b.Property(u => u.Username).HasMaxLength(100);
                b.Property(u => u.Email).HasMaxLength(200);
                b.Property(u => u.FullName).HasMaxLength(200);
            });

            modelBuilder.Entity<Role>(b =>
            {
                b.HasIndex(r => r.Name).IsUnique();
                b.Property(r => r.Name).HasMaxLength(100);
            });
        }
    }
}
