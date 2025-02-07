namespace MyVocabulary.UI;

public partial class AppShell : Shell
{

    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(Pages.SettingsPage), typeof(Pages.SettingsPage));
        Routing.RegisterRoute(nameof(Pages.TopicDetailPage), typeof(Pages.TopicDetailPage));
        Routing.RegisterRoute(nameof(Pages.UserAnswersPage), typeof(Pages.UserAnswersPage));
        Routing.RegisterRoute(nameof(Pages.PhraseDetailPage), typeof(Pages.PhraseDetailPage));
        Routing.RegisterRoute(nameof(Pages.PhrasesPage), typeof(Pages.PhrasesPage));
        Routing.RegisterRoute(nameof(Pages.PhraseUsageDetailPage), typeof(Pages.PhraseUsageDetailPage));
    }

    private void SfSegmentedControl_SelectionChanged(object sender, 
        Syncfusion.Maui.Toolkit.SegmentedControl.SelectionChangedEventArgs e)
    {
        Microsoft.Maui.Controls.Application.Current!.UserAppTheme = e.NewIndex == 0 ? AppTheme.Light : AppTheme.Dark;
    }

}