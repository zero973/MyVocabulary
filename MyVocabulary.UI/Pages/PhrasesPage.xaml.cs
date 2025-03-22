using MyVocabulary.UI.PageModels;

namespace MyVocabulary.UI.Pages;

public partial class PhrasesPage : ContentPage
{
    public PhrasesPage(PhrasesPageModel model)
	{
		InitializeComponent();
        BindingContext = model;
    }
}