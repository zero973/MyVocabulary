using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MyVocabulary.UI.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterPagesAndModels(this IServiceCollection services)
    {
        services.TryAddTransient<Pages.MainPage>();
        services.TryAddTransient<PageModels.MainPageModel>();
        services.TryAddTransient<Pages.SettingsPage>();
        services.TryAddTransient<PageModels.SettingsPageModel>();
        services.TryAddTransient<Pages.TopicDetailPage>();
        services.TryAddTransient<PageModels.TopicDetailPageModel>();
        services.TryAddTransient<Pages.UserAnswersPage>();
        services.TryAddTransient<PageModels.UserAnswersPageModel>();
        services.TryAddTransient<Pages.WordDetailPage>();
        services.TryAddTransient<PageModels.WordDetailPageModel>();
        services.TryAddTransient<Pages.WordsPage>();
        services.TryAddTransient<PageModels.WordsPageModel>();
        services.TryAddTransient<Pages.WordUsageDetailPage>();
        services.TryAddTransient<PageModels.WordUsageDetailPageModel>();
    }
}