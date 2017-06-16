using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Scouts.Core.Model;
using Scouts.Core.Model.Entities;

namespace Scouts.Data
{
    public class AccountContext : BaseContext
    {
        public AccountContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual DbSet<Fee> Fees { get; set; }

        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fee>()
                .ToTable("Fee")
                .HasKey(b => b.FeeId)
                .HasName("PK_Fee");

            modelBuilder.Entity<Transaction>()
                .ToTable("Transaction")
                .HasKey(b => b.TransactionId)
                .HasName("PK_Transaction");

            modelBuilder.Entity<Scout>()
                .Ignore(b => b.Contacts)
                .Ignore(b => b.Activities)
                .ToTable("Scout")
                .HasKey(b => b.ScoutId)
                .HasName("PK_Scout");

        }
    }
}
