﻿using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using MyVocabulary.Application.Commands.Database;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.Languages;
using MyVocabulary.Application.Queries.Topics;
using MyVocabulary.Application.Specifications;
using MyVocabulary.UI.Enums;
using MyVocabulary.UI.NavigationParameters;

namespace MyVocabulary.UI.PageModels;

public partial class MainPageModel(ISender _sender) : ObservableObject
{

    private const int TakeElementsCount = 25;

    private uint PageNumber = 0;

    private bool _isNavigatedTo;

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
    private bool _isLoading = true;

    public bool HasTopics => Topics?.Any() ?? false;

    private async Task LoadData()
    {
        var languages = await _sender.Send(new GetLanguagesRequest());
        _languagesDict = languages.ToDictionary(x => x.Name, x => x);
        Languages = new ObservableCollection<string>(_languagesDict.Keys);

        var topics = await _sender.Send(new GetTopicsRequest(
            new TopicsSpecification(0, TakeElementsCount)));
        Topics = new ObservableCollection<TopicDTO>(topics);

        SelectedLanguageFrom = _languagesDict.First().Key;
        SelectedLanguageTo = _languagesDict.First().Key;

        ShowLoadMoreButton = topics.Value.Any();
    }

    [RelayCommand]
    private void NavigatedTo() => _isNavigatedTo = true;

    [RelayCommand]
    private void NavigatedFrom() => _isNavigatedTo = false;

    [RelayCommand]
    private async Task Appearing()
    {
        IsLoading = true;

        await _sender.Send(new MigrateDatabase());
        await LoadData();

        IsLoading = false;
    }

    [RelayCommand]
    private async Task CreateTopic() 
        => await Shell.Current.GoToAsync(nameof(Pages.TopicDetailPage), 
            new PageNavigationParameter<TopicDTO>(NavigationModes.New));

    [RelayCommand]
    private async Task Tap(TopicDTO topic)
    {
        var topicDto = await _sender.Send(new GetTopicRequest(topic.Id));
        await Shell.Current.GoToAsync(nameof(Pages.TopicDetailPage),
            new PageNavigationParameter<TopicDTO>(NavigationModes.Exists, topicDto));
    }

    [RelayCommand]
    private async Task Search()
    {
        PageNumber = 0;

        var topics = await _sender.Send(new GetTopicsRequest(
            new TopicsSpecification(0, 
                TakeElementsCount, 
                SearchText,
                _languagesDict[SelectedLanguageFrom], 
                _languagesDict[SelectedLanguageTo])));
        Topics = new ObservableCollection<TopicDTO>(topics);

        ShowLoadMoreButton = topics.Value.Any();
    }

    [RelayCommand]
    private async Task LoadMore()
    {
        PageNumber++;

        var topics = await _sender.Send(new GetTopicsRequest(
            new TopicsSpecification(PageNumber, 
                TakeElementsCount, 
                SearchText,
                _languagesDict[SelectedLanguageFrom], 
                _languagesDict[SelectedLanguageTo])));

        ShowLoadMoreButton = topics.Value.Any();

        foreach (var topic in topics.Value)
            Topics.Add(topic);
    }

}