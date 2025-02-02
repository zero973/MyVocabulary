using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MyVocabulary.Infrastructure.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlite("MyVocabulary.db3",
            opt => opt.MigrationsAssembly(typeof(AppDbContext).Assembly));

        return new AppDbContext(optionsBuilder.Options);
    }
}