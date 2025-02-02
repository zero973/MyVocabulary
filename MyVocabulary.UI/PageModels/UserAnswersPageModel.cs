using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.UserAnswers;
using MyVocabulary.Application.Specifications;

namespace MyVocabulary.UI.PageModels;

public partial class UserAnswersPageModel : ObservableObject
{

    /// <summary>
    /// 
    /// </summary>
    private const int TakeElementsCount = 25;

    /// <summary>
    /// 
    /// </summary>
    private uint PageNumber = 0;

    private bool _isNavigatedTo;

    private readonly ISender _sender;

    [ObservableProperty]
    private ObservableCollection<UserAnswerDTO> _userAnswers = [];

    public bool HasAnswers => UserAnswers?.Any() ?? false;

    public UserAnswersPageModel(ISender sender)
    {
        _sender = sender;
    }

    private async Task LoadData()
    {
        var answers = await _sender.Send(new GetUserAnswersRequest(
            new UserAnswersSpecification(PageNumber, TakeElementsCount)));
        UserAnswers = new ObservableCollection<UserAnswerDTO>(answers);
    }

    [RelayCommand]
    private void NavigatedTo() => _isNavigatedTo = true;

    [RelayCommand]
    private void NavigatedFrom() => _isNavigatedTo = false;

    [RelayCommand]
    private async Task Appearing()
    {
        if (!_isNavigatedTo)
            await LoadData();
    }

    [RelayCommand]
    private async Task LoadMore()
    {
        PageNumber++;

        var userAnswers = await _sender.Send(new GetUserAnswersRequest(
            new UserAnswersSpecification(0, TakeElementsCount)));

        foreach (var topic in userAnswers.Value)
            UserAnswers.Add(topic);
    }

}