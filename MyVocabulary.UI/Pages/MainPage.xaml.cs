using Microsoft.Extensions.Logging;
using MyVocabulary.UI.PageModels;

namespace MyVocabulary.UI.Pages;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageModel model, ILogger<MainPage> logger)
	{
		InitializeComponent();
        BindingContext = model;
    }
}