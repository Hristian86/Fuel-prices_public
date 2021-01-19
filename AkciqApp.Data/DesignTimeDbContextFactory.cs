namespace AkciqApp.Data
{
    using System.IO;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        private readonly string herokuPostgres = @"
  Host=ec2-52-214-38-135.eu-west-1.compute.amazonaws.com;
  Port=5432;
  Username=ydzsgbusbhwddf;
  Password=32fccb368f34556bfcabfa653b63ead7899344f8f9bea9e9cccd93ee00ea6897;
  Database=d8n2odgqoinf5e;
  Pooling=true;
  SSL Mode=Require;
  TrustServerCertificate=True;
";
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = herokuPostgres;
            builder.UseSqlServer(configuration.GetConnectionString("ProductionConnection"));

            return new ApplicationDbContext(builder.Options);
        }
    }
}
