using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MyVocabulary.UI.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers application pages and their corresponding PageModels in the dependency injection container.
    /// </summary>
    public static void RegisterPagesAndModels(this IServiceCollection services)
    {
        services.TryAddSingleton<Pages.MainPage>();
        services.TryAddSingleton<PageModels.MainPageModel>();
        
        services.TryAddTransient<Pages.SettingsPage>();
        services.TryAddTransient<PageModels.SettingsPageModel>();
        
        services.TryAddTransient<Pages.TopicDetailPage>();
        services.TryAddTransient<PageModels.TopicDetailPageModel>();
        
        services.TryAddTransient<Pages.UserAnswersPage>();
        services.TryAddTransient<PageModels.UserAnswersPageModel>();
        
        services.TryAddTransient<Pages.PhraseDetailPage>();
        services.TryAddTransient<PageModels.PhraseDetailPageModel>();
        
        services.TryAddTransient<Pages.PhrasesPage>();
        services.TryAddTransient<PageModels.PhrasesPageModel>();
        
        services.TryAddTransient<Pages.PhraseUsageDetailPage>();
        services.TryAddTransient<PageModels.PhraseUsageDetailPageModel>();
        
        services.TryAddTransient<Pages.TopicPracticePage>();
        services.TryAddTransient<PageModels.TopicPracticePageModel>();
        
        services.TryAddTransient<Pages.LessonPage>();
        services.TryAddTransient<PageModels.LessonPageModel>();
    }
}