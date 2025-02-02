using MyVocabulary.UI.PageModels;

namespace MyVocabulary.UI.Pages;

public partial class MainPage : ContentPage
{

    private readonly MainPageModel _model;

    public MainPage(MainPageModel model)
	{
		InitializeComponent();
        BindingContext = _model = model;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args) 
        => _model.NavigatedToCommand.Execute(args);

    protected override void OnNavigatingFrom(NavigatingFromEventArgs args)
        => _model.NavigatedFromCommand.Execute(args);

    protected async override void OnAppearing() 
        => await _model.AppearingCommand.ExecuteAsync(null);

}