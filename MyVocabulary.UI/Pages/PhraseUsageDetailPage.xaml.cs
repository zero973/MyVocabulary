using MyVocabulary.UI.PageModels;

namespace MyVocabulary.UI.Pages;

public partial class PhraseUsageDetailPage : ContentPage
{
    public PhraseUsageDetailPage(PhraseUsageDetailPageModel model)
	{
		InitializeComponent();
        BindingContext = model;
    }
}