using App.Domain.Shared;
using App.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Shared
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.AutoAddDbSetClass<IEntity>(typeof(IEntity).Assembly);
            modelBuilder.AddPluralizingTableNameConvention();
            modelBuilder.ForSqlServerUseSequenceHiLo("DBSequenceHiLoObject");
            modelBuilder.SetUpSequentialGuid();
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
