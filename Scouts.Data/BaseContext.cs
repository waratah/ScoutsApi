using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Scouts.Core.Model;

namespace Scouts.Data
{
    public class BaseContext : DbContext
    {
        protected IConfiguration _configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer( _configuration.GetConnectionString("default"));
            base.OnConfiguring(optionsBuilder);
        }

        public virtual void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }
    }
}
