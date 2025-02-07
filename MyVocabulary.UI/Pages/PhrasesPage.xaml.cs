using MyVocabulary.UI.PageModels;

namespace MyVocabulary.UI.Pages;

public partial class PhrasesPage : ContentPage
{

    private readonly PhrasesPageModel _model;

    public PhrasesPage(PhrasesPageModel model)
	{
		InitializeComponent();
        BindingContext = _model = model;
    }

    protected async override void OnAppearing()
        => await _model.AppearingCommand.ExecuteAsync(null);

}