using MyVocabulary.UI.PageModels;

namespace MyVocabulary.UI.Pages;

public partial class PhraseUsageDetailPage : ContentPage
{

    private readonly PhraseUsageDetailPageModel _model;

    public PhraseUsageDetailPage(PhraseUsageDetailPageModel model)
	{
		InitializeComponent();
        BindingContext = _model = model;
    }

    protected async override void OnAppearing()
        => await _model.AppearingCommand.ExecuteAsync(null);

}