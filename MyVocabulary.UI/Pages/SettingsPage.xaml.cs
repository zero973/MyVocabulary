using MyVocabulary.UI.PageModels;

namespace MyVocabulary.UI.Pages;

public partial class SettingsPage : ContentPage
{

    private readonly SettingsPageModel _model;

    public SettingsPage(SettingsPageModel model)
	{
		InitializeComponent();
        BindingContext = _model = model;
    }

    protected async override void OnAppearing()
        => await _model.AppearingCommand.ExecuteAsync(null);

}