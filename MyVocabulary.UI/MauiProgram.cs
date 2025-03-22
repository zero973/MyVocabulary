using CommunityToolkit.Maui;
using Fonts;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyVocabulary.Application.Commands.App;
using MyVocabulary.Application.Commands.Database;
using MyVocabulary.Domain.Interfaces;
using MyVocabulary.UI.Extensions;
using NLog;
using NLog.Extensions.Logging;
using NLog.Layouts;
using Syncfusion.Maui.Toolkit.Hosting;

namespace MyVocabulary.UI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var layout =
            new SimpleLayout(
                @"${date:format=dd.MM.yyyy HH\:mm\:ss} (${level:uppercase=true}): ${message}. ${exception:format=ToString}");
        LogManager.Setup().RegisterMauiLog((_, ex) =>
            {
                var exception = ex.ExceptionObject as Exception ?? new Exception("Unknown Unhandled Exception");
                LogManager.GetCurrentClassLogger().Fatal(exception, "Unhandled Exception");
            })
            .LoadConfiguration(c => c.ForLogger()
                .FilterMinLevel(NLog.LogLevel.Info)
                .WriteToDebug(layout)
                .WriteToMauiLog(layout))
            .GetCurrentClassLogger();

        TaskScheduler.UnobservedTaskException += (_, args) =>
        {
            LogManager.GetCurrentClassLogger().Fatal(args.Exception, "Unobserved Task Exception");
        };
        
        var builder = MauiApp.CreateBuilder();
        
        builder.Logging.ClearProviders();
        builder.Logging.AddNLog();
        
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureSyncfusionToolkit()
            .ConfigureMauiHandlers(handlers => { })
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

        var installers = new List<IModuleInstaller>
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

        builder.Services.RegisterPagesAndModels();

        var app = builder.Build();

        PerformStartupTasks(app);

        return app;
    }

    private static void PerformStartupTasks(MauiApp app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        var sender = scope.ServiceProvider.GetRequiredService<ISender>();

        sender.Send(new MigrateDatabase()).GetAwaiter().GetResult();

        var userSettings = sender.Send(new LoadUserSettingsRequest()).GetAwaiter().GetResult();

        Thread.CurrentThread.CurrentCulture = userSettings.Value.AppLanguage.Culture;
        Thread.CurrentThread.CurrentUICulture = userSettings.Value.AppLanguage.Culture;
    }
}