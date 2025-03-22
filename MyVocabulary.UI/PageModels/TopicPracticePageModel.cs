using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using MyVocabulary.Application.Enums;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.TopicPractice;
using MyVocabulary.UI.NavigationParameters;

namespace MyVocabulary.UI.PageModels;

public partial class TopicPracticePageModel(ISender sender) : ObservableObject, IQueryAttributable
{
    
    [ObservableProperty]
    private TopicDTO _topic = null!;

    [ObservableProperty]
    private string _studySummaryText = null!;

    [ObservableProperty]
    private string _studyPercentText = null!;
    
    [ObservableProperty]
    private double _studyProgress;
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CountPhraseUsagesToStudyText))]
    private uint _countPhraseUsagesToStudy;

    public string CountPhraseUsagesToStudyText => $"Count phrase usages to study: {CountPhraseUsagesToStudy}";

    [ObservableProperty]
    private ObservableCollection<string> _practiceVariants = null!;

    [ObservableProperty]
    private int _selectedPracticeVariant;
    
    private TopicPracticeResult _topicPracticeResult = null!;
    
    private Dictionary<StudyVariants, string> _studyVariants = null!;
    
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.Any())
            return;
        
        var parameters = SimpleNavigationParameter<TopicDTO>.From(query);
        Topic = parameters.Value;
    }

    [RelayCommand]
    private async Task Appearing()
    {
        _studyVariants = await sender.Send(new GetStudyVariantsRequest());
        PracticeVariants = new ObservableCollection<string>(_studyVariants.Values);
        SelectedPracticeVariant = 1;
        SelectedPracticeVariant = 0;
        
        _topicPracticeResult = await sender.Send(new GetTopicPracticeResultRequest(Topic));
        StudySummaryText = $"Total phrase usages: {Topic.PhraseUsages.Count}. Correct answers: {_topicPracticeResult.CorrectAnswers}. Wrong answers: {_topicPracticeResult.WrongAnswers}";
        StudyPercentText = $"Topic learned to {_topicPracticeResult.StudyProgressPercent}%";
        StudyProgress = _topicPracticeResult.StudyProgress;
    }
    
    [RelayCommand]
    private async Task StartLesson()
    {
        if (SelectedPracticeVariant == (int)StudyVariants.FixMistakes && _topicPracticeResult.WrongAnswers == 0)
        {
            await Toast.Make("We can't fix mistakes because they don't exist", ToastDuration.Long).Show();
            return;
        }
        
        if (SelectedPracticeVariant == (int)StudyVariants.LearnNewWords && StudyProgress == 1)
        {
            await Toast.Make("You already learnt all words").Show();
            return;
        }
        
        if (CountPhraseUsagesToStudy > Topic.PhraseUsages.Count || CountPhraseUsagesToStudy == 0)
        {
            var msg = "You cant write bigger count than count of phrase usages in this topic";
            if (CountPhraseUsagesToStudy == 0)
                msg = "Count of phrase usages can't be zero";
            
            await Toast.Make(msg, ToastDuration.Long).Show();
            return;
        }

        if (CountPhraseUsagesToStudy < 5)
        {
            await Toast.Make("Count of words to study must be bigger than 4").Show();
            return;
        }
        
        var phraseUsagesForPractice = await sender.Send(
            new GetPhraseUsagesForPracticeRequest((StudyVariants)SelectedPracticeVariant, 
                CountPhraseUsagesToStudy, Topic));
        if (!phraseUsagesForPractice.IsSuccess)
        {
            await Toast.Make(string.Join("; ", phraseUsagesForPractice.Errors), ToastDuration.Long).Show();
            return;
        }
        
        await Shell.Current.GoToAsync(nameof(Pages.LessonPage), 
            new LessonNavigationParameter(phraseUsagesForPractice.Value.OriginalPhrases, 
                phraseUsagesForPractice.Value.ReversedPhrases));
    }
    
}