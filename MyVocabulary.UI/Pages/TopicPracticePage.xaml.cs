using MyVocabulary.UI.PageModels;

namespace MyVocabulary.UI.Pages;

public partial class TopicPracticePage : ContentPage
{
    public TopicPracticePage(TopicPracticePageModel model)
    {
        InitializeComponent();
        BindingContext = model;
    }
}