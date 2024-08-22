using Microsoft.EntityFrameworkCore;
using MosadRest.Models;

namespace MosadRest.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<AgentModel> Agents { get; set; }
        public DbSet<TargetModel> Targets { get; set; }
        public DbSet<MissionModel> Missions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgentModel>()
                .Property(a => a.Status)
                .HasConversion<string>()
                .IsRequired();

            modelBuilder.Entity<TargetModel>()
                .Property(t => t.Status)
                .HasConversion<string>()
                .IsRequired();

            modelBuilder.Entity<MissionModel>()
                .Property(a => a.MissionStatus)
                .HasConversion<string>()
                .IsRequired();

            modelBuilder.Entity<MissionModel>()
                .HasOne(m => m.Agent)
                .WithMany(a => a.Missions)
                .HasForeignKey(m => m.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MissionModel>()
                .HasOne(m => m.Target)
                .WithMany(a => a.Missions)
                .HasForeignKey(m => m.TargetId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}