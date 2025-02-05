using MyVocabulary.UI.PageModels;

namespace MyVocabulary.UI.Pages;

public partial class TopicDetailPage : ContentPage
{

    private readonly TopicDetailPageModel _model;

    public TopicDetailPage(TopicDetailPageModel model)
	{
		InitializeComponent();
        BindingContext = _model = model;
    }

    protected async override void OnAppearing()
        => await _model.AppearingCommand.ExecuteAsync(null);

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
        => await _model.NavigatedToCommand.ExecuteAsync(args);

}