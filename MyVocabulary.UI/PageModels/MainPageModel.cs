using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.Languages;
using MyVocabulary.Application.Queries.Topics;
using MyVocabulary.Application.Specifications;
using MyVocabulary.UI.Enums;
using MyVocabulary.UI.NavigationParameters;

namespace MyVocabulary.UI.PageModels;

public partial class MainPageModel(ISender sender) : ObservableObject
{

    private const int TakeElementsCount = 25;

    private uint _pageNumber = 0;

    /// <summary>
    /// Languages dictionary
    /// </summary>
    private Dictionary<string, Language> _languagesDict = null!;

    /// <summary>
    /// Language names
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<string> _languages = [];

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasTopics))]
    private ObservableCollection<TopicDTO> _topics = [];

    [ObservableProperty]
    private string _searchText = "";

    [ObservableProperty]
    private string _selectedLanguageFrom = null!;

    [ObservableProperty]
    private string _selectedLanguageTo = null!;

    [ObservableProperty]
    private bool _showLoadMoreButton = true;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ArrowIcon))]
    private bool _isOneWaySearch = true;

    [ObservableProperty]
    private bool _isLoading = true;

    public bool HasTopics => Topics?.Any() ?? false;

    public ImageSource ArrowIcon => (ImageSource)Microsoft.Maui.Controls.Application.Current!.Resources
        [IsOneWaySearch ? "RightArrow" : "BidirectionalArrow"];

    private async Task LoadData()
    {
        var languages = await sender.Send(new GetSortedLanguagesRequest());
        _languagesDict = languages.ToDictionary(x => x.Name, x => x);
        Languages = new ObservableCollection<string>(_languagesDict.Keys);

        var topics = await sender.Send(new GetTopicsRequest(
            new TopicsSpecification(0, TakeElementsCount)));
        Topics = new ObservableCollection<TopicDTO>(topics);

        SelectedLanguageFrom = _languagesDict.First().Key;
        SelectedLanguageTo = _languagesDict.First().Key;

        ShowLoadMoreButton = topics.Value.Count != 0;
    }

    [RelayCommand]
    private async Task Appearing()
    {
        IsLoading = true;
        
        await LoadData();
        
        IsLoading = false;
    }

    [RelayCommand]
    private async Task CreateTopic() 
        => await Shell.Current.GoToAsync(nameof(Pages.TopicDetailPage), 
            new PageNavigationParameter<TopicDTO>(NavigationTypes.Create, null));

    [RelayCommand]
    private async Task Tap(TopicDTO topic)
    {
        IsLoading = true;

        var topicDto = await sender.Send(new GetTopicRequest(topic.Id));
        await Shell.Current.GoToAsync(nameof(Pages.TopicDetailPage),
            new PageNavigationParameter<TopicDTO>(NavigationTypes.Open, topicDto));

        IsLoading = false;
    }

    [RelayCommand]
    private async Task Search()
    {
        _pageNumber = 0;

        var topics = await sender.Send(new GetTopicsRequest(
            new TopicsSpecification(0, 
                TakeElementsCount, 
                SearchText,
                _languagesDict[SelectedLanguageFrom], 
                _languagesDict[SelectedLanguageTo], 
                IsOneWaySearch)));
        Topics = new ObservableCollection<TopicDTO>(topics);

        ShowLoadMoreButton = topics.Value.Count != 0;
    }

    [RelayCommand]
    private async Task LoadMore()
    {
        _pageNumber++;

        var topics = await sender.Send(new GetTopicsRequest(
            new TopicsSpecification(_pageNumber, 
                TakeElementsCount, 
                SearchText,
                _languagesDict[SelectedLanguageFrom], 
                _languagesDict[SelectedLanguageTo],
                IsOneWaySearch)));

        ShowLoadMoreButton = topics.Value.Count != 0;

        foreach (var topic in topics.Value)
            Topics.Add(topic);
    }

    [RelayCommand]
    private void ToggleSearchDirection() => IsOneWaySearch = !IsOneWaySearch;

}