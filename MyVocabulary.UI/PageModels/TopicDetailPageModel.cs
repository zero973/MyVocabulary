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
using MyVocabulary.Application.Queries.Topics;
using MyVocabulary.UI.Enums;
using MyVocabulary.UI.Extensions;
using MyVocabulary.UI.Localization;
using MyVocabulary.UI.NavigationParameters;

namespace MyVocabulary.UI.PageModels;

public partial class TopicDetailPageModel(ISender _sender) : ObservableObject, IQueryAttributable
{

    // todo replace it somewhere
    private const string NoImageUrl = "https://sun9-58.userapi.com/impg/T2LyUzjz8C3DKtVoI7p6fo2edXNP04AOcYsDZQ/2Pj8k46yrqk.jpg?size=383x341&quality=95&sign=3f0a715c29c549d26b93b073a344df45";

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

    public bool HasWordUsages => (Topic?.PhraseUsages?.Count ?? 0) > 0;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var parameters = PageNavigationParameter<TopicDTO>.From(query);
        if (parameters.Mode == NavigationModes.Exists)
        {
            Topic = parameters.Value!;
        }
        else
        {
            IsCreateMode = true;
            Topic = new TopicDTO(Guid.NewGuid(), Language.Default(), Language.Default(), 
                "", "", NoImageUrl, new List<PhraseUsageDTO>());
        }
        PictureUrl = Topic.PhotoUrl;
    }

    [RelayCommand]
    private async Task Appearing()
    {
        await LoadData();
    }

    [RelayCommand]
    private async Task NavigatedTo()
    {
        if (!IsCreateMode)
            Topic = await _sender.Send(new GetTopicRequest(Topic.Id));
    }

    [RelayCommand]
    private async Task ChangeImage()
    {
        var imageUrl = await Shell.Current.ShowPopupAsync(new Controls.ChooseImagePopup(PictureUrl)) as string;
        PictureUrl = imageUrl;
    }

    [RelayCommand]
    private async Task SaveTopic()
    {
        var ensure = await Shell.Current.DisplayAlert(AppResources.Attention,
            "Are you sure that you want to save this topic ?", AppResources.Yes, AppResources.No);

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
                await Shell.Current.DisplayAlert(AppResources.Error, $"You can't create topic: {message}", AppResources.Ok);
                return;
            }

            await Toast.Make("Topic successfully created", ToastDuration.Short).Show();

            Topic.Id = result.Value.Id;
            IsCreateMode = false;
        }
        else
        {
            var result = await _sender.Send(new EditTopicRequest(Topic));

            if (!result.IsSuccess)
            {
                var message = Environment.NewLine + string.Join(";" + Environment.NewLine, result.Errors);
                await Shell.Current.DisplayAlert(AppResources.Error, $"You can't edit topic: {message}", AppResources.Ok);
                return;
            }

            await Toast.Make("Topic successfully edited", ToastDuration.Short).Show();
        }
    }

    [RelayCommand]
    private async Task DeleteTopic()
    {
        var ensure = await Shell.Current.DisplayAlert(AppResources.Attention, 
            "Are you sure that you want to delete this topic ?", AppResources.Yes, AppResources.No);

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
    private async Task StartLesson()
    {
        if (Topic.PhraseUsages.Count == 0)
        {
            await Toast.Make("We can't start lesson - no word usages in topic", ToastDuration.Long).Show();
            return;
        }

        await Toast.Make("Coming soon !", ToastDuration.Short).Show();
    }

    [RelayCommand]
    private async Task CreateWordUsage()
    {
        if (IsCreateMode)
        {
            await Shell.Current.DisplayAlert(AppResources.Error,
                "Before you create a word usage, you must save the current topic", AppResources.Ok);
            return;
        }

        await Shell.Current.GoToAsync(nameof(Pages.PhraseUsageDetailPage),
            new PhraseUsageNavigationParameter(NavigationModes.New, Topic));
    }

    [RelayCommand]
    private async Task Tap(PhraseUsageDTO phraseUsage)
    {
        await Shell.Current.GoToAsync(nameof(Pages.PhraseUsageDetailPage),
            new PhraseUsageNavigationParameter(NavigationModes.Exists, Topic, phraseUsage));
    }

    private async Task LoadData()
    {
        var languages = await _sender.Send(new GetSortedLanguagesRequest());
        _languagesDict = languages.ToDictionary(x => x.Name, x => x);
        Languages = new ObservableCollection<string>(_languagesDict.Keys);

        SelectedLanguageFrom = Topic.CultureFrom.Name;
        SelectedLanguageTo = Topic.CultureTo.Name;
    }

}