using MyVocabulary.UI.PageModels;

namespace MyVocabulary.UI.Pages;

public partial class SettingsPage : ContentPage
{
    public SettingsPage(SettingsPageModel model)
	{
		InitializeComponent();
        BindingContext = model;
    }
}