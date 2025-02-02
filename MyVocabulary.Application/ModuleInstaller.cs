using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyVocabulary.Application.Behaviors;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application;

public class ModuleInstaller : IModuleInstaller
{

    public byte Order => 2;

    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddValidatorsFromAssemblyContaining<ModuleInstaller>();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(ModuleInstaller).Assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
    }

}