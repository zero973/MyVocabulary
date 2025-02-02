using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using MyVocabulary.Application.Commands.Topics;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.Languages;
using MyVocabulary.UI.Enums;
using MyVocabulary.UI.Extensions;
using MyVocabulary.UI.NavigationParameters;

namespace MyVocabulary.UI.PageModels;

public partial class TopicDetailPageModel(ISender sender) : ObservableObject, IQueryAttributable
{

    private const string NoImageUrl = "https://sun9-58.userapi.com/impg/T2LyUzjz8C3DKtVoI7p6fo2edXNP04AOcYsDZQ/2Pj8k46yrqk.jpg?size=383x341&quality=95&sign=3f0a715c29c549d26b93b073a344df45";

    private readonly ISender _sender = sender;

    /// <summary>
    /// Languages dictionary
    /// </summary>
    private Dictionary<string, Language> _languagesDict = null!;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasWordUsages))]
    private TopicDTO _topic = null!;

    [ObservableProperty]
    private ObservableCollection<string> _languages = [];

    [ObservableProperty]
    private string _selectedLanguageFrom = null!;

    [ObservableProperty]
    private string _selectedLanguageTo = null!;

    [ObservableProperty]
    private string? _pictureUrl;

    [ObservableProperty]
    public bool _isCreateMode = false;

    public bool HasWordUsages => (Topic?.WordUsages?.Count ?? 0) > 0;

    private async Task LoadData()
    {
        var languages = await _sender.Send(new GetLanguagesRequest());
        _languagesDict = languages.ToDictionary(x => x.Name, x => x);
        Languages = new ObservableCollection<string>(_languagesDict.Keys);

        SelectedLanguageFrom = Topic.CultureFrom.Name;
        SelectedLanguageTo = Topic.CultureTo.Name;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var parameters = PageNavigationParameter<TopicDTO>.From(query);
        if (parameters.Mode == NavigationModes.Exists)
        {
            Topic = parameters.Value!;
            PictureUrl = Topic.PhotoUrl;
        }
        else
        {
            IsCreateMode = true;
            Topic = new TopicDTO(Guid.NewGuid(), Language.Default(), Language.Default(), 
                "", "", NoImageUrl, new List<WordUsageDTO>());
        }
    }

    [RelayCommand]
    private async Task Appearing()
    {
        await LoadData();
    }

    [RelayCommand]
    private async Task ChangeImage()
    {
        var imageUrl = await Shell.Current.ShowPopupAsync(new Controls.ChooseImagePopup()) as string;
        PictureUrl = imageUrl;
    }

    [RelayCommand]
    private async Task SaveTopic()
    {
        var ensure = await Shell.Current.DisplayAlert("Attention",
            "Are you sure that you want to save this topic ?", "Yes", "No");

        if (!ensure)
            return;

        Topic.CultureFrom = _languagesDict[SelectedLanguageFrom];
        Topic.CultureTo = _languagesDict[SelectedLanguageTo];
        Topic.PhotoUrl = PictureUrl;

        if (IsCreateMode)
        {
            var result = await _sender.Send(new AddTopicRequest(Topic));

            if (!result.IsSuccess)
            {
                var message = Environment.NewLine + string.Join(";" + Environment.NewLine, result.Errors);
                await Shell.Current.DisplayAlert("Error", $"You can't create topic: {message}", "Ok");
                return;
            }

            await Toast.Make("Topic successfully created", ToastDuration.Short).Show();
        }
        else
        {
            var result = await _sender.Send(new EditTopicRequest(Topic));

            if (!result.IsSuccess)
            {
                var message = Environment.NewLine + string.Join(";" + Environment.NewLine, result.Errors);
                await Shell.Current.DisplayAlert("Error", $"You can't edit topic: {message}", "Ok");
                return;
            }

            await Toast.Make("Topic successfully edited", ToastDuration.Short).Show();
        }
    }

    [RelayCommand]
    private async Task DeleteTopic()
    {
        var ensure = await Shell.Current.DisplayAlert("Attention", 
            "Are you sure that you want to delete this topic ?", "Yes", "No");

        if (!ensure)
            return;

        if (IsCreateMode)
        {
            await Shell.Current.GoBack();
            return;
        }

        await _sender.Send(new DeleteTopicRequest(Topic.Id));
        await Toast.Make("Topic successfully deleted", ToastDuration.Short).Show();
        await Shell.Current.GoBack();
    }

    [RelayCommand]
    private async Task StartLeson()
    {

    }

    [RelayCommand]
    private async Task CreateWordUsage()
        => await Shell.Current.GoToAsync(nameof(Pages.WordUsageDetailPage),
            new PageNavigationParameter<WordUsageDTO>(NavigationModes.New));

    [RelayCommand]
    private async Task Tap(WordUsageDTO wordUsage)
        => await Shell.Current.GoToAsync(nameof(Pages.WordUsageDetailPage),
            new PageNavigationParameter<WordUsageDTO>(NavigationModes.Exists, wordUsage));

}