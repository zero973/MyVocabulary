using Microsoft.EntityFrameworkCore;

namespace MyVocabulary.Infrastructure.Data;

public class AppDbContext : DbContext
{



    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
        // dotnet tool update --global dotnet-ef
        // dotnet ef migrations add init -c AppDbContext --output-dir Migrations
        // dotnet ef database update -c AppDbContext
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
    }

}