using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyVocabulary.Domain.Interfaces;
using MyVocabulary.Infrastructure.Data;

namespace MyVocabulary.Infrastructure;

public class ModuleInstaller : IModuleInstaller
{

    public byte Order => 1;

    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opts =>
        {
            opts.UseSqlite(configuration.GetConnectionString("Sqlite"), options =>
            {
                options.MigrationsAssembly(typeof(AppDbContext).Assembly);
            });
        });

        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(ModuleInstaller).Assembly);
        });
    }

}