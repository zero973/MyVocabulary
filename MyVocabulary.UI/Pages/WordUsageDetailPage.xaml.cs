using MyVocabulary.UI.PageModels;

namespace MyVocabulary.UI.Pages;

public partial class WordUsageDetailPage : ContentPage
{

    private readonly WordUsageDetailPageModel _model;

    public WordUsageDetailPage(WordUsageDetailPageModel model)
	{
		InitializeComponent();
        BindingContext = _model = model;
    }

    protected async override void OnAppearing()
        => await _model.AppearingCommand.ExecuteAsync(null);

}