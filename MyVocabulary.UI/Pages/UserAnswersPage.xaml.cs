using MyVocabulary.UI.PageModels;

namespace MyVocabulary.UI.Pages;

public partial class UserAnswersPage : ContentPage
{
	public UserAnswersPage(UserAnswersPageModel model)
	{
		InitializeComponent();
		BindingContext = model;
	}
}