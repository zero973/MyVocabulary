using Microsoft.Extensions.Logging;
using MyVocabulary.UI.PageModels;

namespace MyVocabulary.UI.Pages;

public partial class TopicDetailPage : ContentPage
{
    public TopicDetailPage(TopicDetailPageModel model)
	{
        InitializeComponent();
        BindingContext = model;
    }
}