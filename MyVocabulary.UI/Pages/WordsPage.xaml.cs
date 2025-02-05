using MyVocabulary.UI.PageModels;

namespace MyVocabulary.UI.Pages;

public partial class WordsPage : ContentPage
{

    private readonly WordsPageModel _model;

    public WordsPage(WordsPageModel model)
	{
		InitializeComponent();
        BindingContext = _model = model;
    }

    protected async override void OnAppearing()
        => await _model.AppearingCommand.ExecuteAsync(null);

}