using CommunityToolkit.Maui;
using Fonts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.RegisterPagesAndModels();

        return builder.Build();
    }
}