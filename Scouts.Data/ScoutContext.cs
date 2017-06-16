using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Scouts.Core.Model;

namespace Scouts.Data
{
    public class ScoutContext : BaseContext
    {
        public ScoutContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual DbSet<Scout> Scouts { get; set; }

        public virtual DbSet<Section> Sections { get; set; }

        public virtual DbSet<Activity> Activities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ensure we don't go through all the linked tables
            modelBuilder.Entity<Scout>()
                .ToTable("Scout")
                .HasKey(b => b.ScoutId)
                .HasName("PK_Scout")
                ;

            modelBuilder.Entity<ActivityJoinScouts>()
                .HasKey(b => new { b.ScoutId, b.ActivityId });
            modelBuilder.Entity<ActivityJoinScouts>()
                .HasOne(pt => pt.Scout)
                .WithMany(p => p.Activities)
                .HasForeignKey(pt => pt.ActivityId);

            modelBuilder.Entity<ActivityJoinScouts>()
               .HasOne(pt => pt.Activity)
               .WithMany(p => p.Scouts)
               .HasForeignKey(pt => pt.ScoutId);

            modelBuilder.Entity<Section>()
                .ToTable("Section")
                .HasKey(b => b.SectionId)
                .HasName("PK_Section");

            modelBuilder.Entity<Activity>()
                .ToTable("Activity")
                .HasKey(b => b.ActivityId)
                .HasName("PK_Activity");

            modelBuilder.Entity<ScoutContact>()
               .HasKey(b => new { b.ScoutId, b.ContactId });
            modelBuilder.Entity<ScoutContact>()
               .HasOne(pt => pt.Scout)
               .WithMany(p => p.Contacts)
               .HasForeignKey(pt => pt.ScoutId);

            modelBuilder.Entity<ScoutContact>()
               .HasOne(pt => pt.Contact)
               .WithMany(p => p.Scouts)
               .HasForeignKey(pt => pt.ContactId);
        }
    }
}
