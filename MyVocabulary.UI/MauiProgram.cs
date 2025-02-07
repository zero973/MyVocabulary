using CommunityToolkit.Maui;
using Fonts;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyVocabulary.Application.Commands.App;
using MyVocabulary.Application.Commands.Database;
using MyVocabulary.Domain.Interfaces;
using MyVocabulary.UI.Extensions;
using Syncfusion.Maui.Toolkit.Hosting;

namespace MyVocabulary.UI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureSyncfusionToolkit()
            .ConfigureMauiHandlers(handlers =>
            {
            })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("SegoeUI-Semibold.ttf", "SegoeSemibold");
                fonts.AddFont("FluentSystemIcons-Regular.ttf", FluentUI.FontFamily);
            });

        var settings = new Dictionary<string, string>
        {
            { "ConnectionStrings:Sqlite", "Filename=" + Path.Combine(FileSystem.AppDataDirectory, "MyVocabulary.db3") }
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(settings!)
            .Build();

        builder.Services.AddSingleton<IConfiguration>(configuration);

        var installers = new List<IModuleInstaller>()
        {
            new Infrastructure.ModuleInstaller(),
            new Application.ModuleInstaller()
        };

        foreach (var installer in installers.OrderBy(x => x.Order))
            installer.Install(builder.Services, configuration);

        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(MauiProgram).Assembly);
        });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.RegisterPagesAndModels();

        var app = builder.Build();

        SetAppCulture(app);

        return app;
    }

    private static void SetAppCulture(MauiApp app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        var sender = scope.ServiceProvider.GetRequiredService<ISender>();

        Task.Run(() => sender.Send(new MigrateDatabase()));

        var userSettings = Task.Run(() => sender.Send(new LoadUserSettingsRequest())).Result;

        Thread.CurrentThread.CurrentCulture = userSettings.Value.AppLanguage.Culture;
        Thread.CurrentThread.CurrentUICulture = userSettings.Value.AppLanguage.Culture;
    }
}