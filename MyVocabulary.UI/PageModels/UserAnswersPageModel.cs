using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.UserAnswers;
using MyVocabulary.Application.Specifications;
using MyVocabulary.UI.Localization;

namespace MyVocabulary.UI.PageModels;

public partial class UserAnswersPageModel(ISender sender) : ObservableObject
{

    /// <summary>
    /// 
    /// </summary>
    private const int TakeElementsCount = 25;

    /// <summary>
    /// 
    /// </summary>
    private uint PageNumber = 0;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasAnswers))]
    private ObservableCollection<UserAnswerDTO> _userAnswers = [];

    public bool HasAnswers => UserAnswers?.Any() ?? false;

    [RelayCommand]
    private async Task Appearing()
    {
        await LoadData();
    }

    [RelayCommand]
    private async Task LoadMore()
    {
        PageNumber++;

        var userAnswers = await sender.Send(new GetUserAnswersRequest(
            new UserAnswersSpecification(0, TakeElementsCount)));

        foreach (var topic in userAnswers.Value)
            UserAnswers.Add(topic);
    }

    [RelayCommand]
    private async Task Tap(UserAnswerDTO answer)
    {
        var ensure = await Shell.Current.DisplayAlert(AppResources.Attention,
            $"Do you want to delete this answer ({answer.Date} - {answer.IsRight}) ?", 
            AppResources.Yes, AppResources.No);
    }

    private async Task LoadData()
    {
        var answers = (await sender.Send(new GetUserAnswersRequest(
            new UserAnswersSpecification(PageNumber, TakeElementsCount)))).Value
                .OrderBy(x => x.PhraseUsage.NativePhrase.Value);
        UserAnswers = new ObservableCollection<UserAnswerDTO>(answers);
    }

}