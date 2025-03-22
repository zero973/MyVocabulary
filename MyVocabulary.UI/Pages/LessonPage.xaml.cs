using MyVocabulary.UI.PageModels;

namespace MyVocabulary.UI.Pages;

public partial class LessonPage : ContentPage
{
    public LessonPage(LessonPageModel model)
    {
        InitializeComponent();
        BindingContext = model;
    }
}