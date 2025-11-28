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

        // View-mapped entity
        public DbSet<AuditFinalizedSummary> AuditFinalizedSummaries { get; set; } = null!;

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

            // Configure view entity as keyless and map to view name
            modelBuilder.Entity<AuditFinalizedSummary>(eb =>
            {
                eb.HasNoKey();
                eb.ToView("vw_AuditFinalizedSummary");
                eb.Property(e => e.AuditId);
                eb.Property(e => e.Title).HasMaxLength(200);
                eb.Property(e => e.Area).HasMaxLength(100);
                eb.Property(e => e.StartDate);
                eb.Property(e => e.EndDate);
                eb.Property(e => e.ResponsibleId);
                eb.Property(e => e.ResponsibleName).HasMaxLength(150);
                eb.Property(e => e.TotalFindings);
                eb.Property(e => e.FindingsBaja);
                eb.Property(e => e.FindingsMedia);
                eb.Property(e => e.FindingsAlta);
                eb.Property(e => e.FindingsWithCompletedFollowUp);
                eb.Property(e => e.PercentFindingsWithCompletedFollowUp).HasColumnType("decimal(5,2)");
            });
        }
    }
}
