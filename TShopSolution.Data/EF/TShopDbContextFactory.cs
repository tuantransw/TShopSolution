using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TShopSolution.Data.EF
{
    public class TShopDbContextFactory : IDesignTimeDbContextFactory<TShopDbContext>
    {
        public TShopDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("TShopSolutionDb");

            var optionsBuilder = new DbContextOptionsBuilder<TShopDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new TShopDbContext(optionsBuilder.Options);
        }
    }
}
