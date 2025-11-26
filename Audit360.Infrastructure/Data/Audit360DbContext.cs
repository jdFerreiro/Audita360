using Audit360.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Audit360.Infrastructure.Data
{
    public class Audit360DbContext(DbContextOptions<Audit360DbContext> options) : DbContext(options)
    {
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

            // Optional: set max lengths and indexes
            modelBuilder.Entity<User>(b =>
            {
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
