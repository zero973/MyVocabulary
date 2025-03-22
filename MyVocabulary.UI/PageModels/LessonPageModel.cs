using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Maui.Controls;
using MyVocabulary.Application.Commands.UserAnswers;
using MyVocabulary.Application.Models;
using MyVocabulary.UI.Enums;
using MyVocabulary.UI.Extensions;
using MyVocabulary.UI.Localization;
using MyVocabulary.UI.NavigationParameters;

namespace MyVocabulary.UI.PageModels;

public partial class LessonPageModel(ISender sender) : ObservableObject, IQueryAttributable
{
    
    [ObservableProperty]
    private PhraseUsageDTO _currentPhrase = null!;

    public string InstructionText => IsLearningStage ? 
        AppResources.RememberTranslation : AppResources.ChooseCorrectTranslation;
    
    [ObservableProperty]
    private string _selectedAnswer = "Cat";
    
    [ObservableProperty]
    private string[] _answers = ["Duck", "Cat", "Dog", "Bird"];
    
    [ObservableProperty]
    private double _studyProgress;

    public bool IsLearningStage => StudyStage == StudyStages.Learning;
    
    [NotifyPropertyChangedFor(nameof(InstructionText))]
    [NotifyPropertyChangedFor(nameof(IsLearningStage))]
    [ObservableProperty]
    private StudyStages _studyStage = StudyStages.Learning;

    public string RadioGroupName => "Answers";
    
    private Random _random = new();
    
    private List<PhraseUsageDTO> _originalPhrases = null!;
    
    private List<PhraseUsageDTO> _reversedPhrasess = null!;
    
    private List<UserAnswerDTO> _userAnswers = [];
    
    private int _currentPhraseIndex;
    
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var parameters = LessonNavigationParameter.From(query);
        _originalPhrases = parameters.OriginalPhrases;
        _reversedPhrasess = parameters.ReversedPhrases;
        CurrentPhrase = _originalPhrases[_currentPhraseIndex];
    }

    [RelayCommand]
    private async Task Appearing()
    {
        
    }

    [RelayCommand]
    private async Task Continue()
    {
        // handle answer and update progress
        var maxProgress = _originalPhrases.Count * 3;
        if (StudyStage == StudyStages.Learning)
        {
            StudyProgress = (double)(_currentPhraseIndex + 1) / maxProgress;
        }
        else
        {
            var isRight = SelectedAnswer == CurrentPhrase.TranslationPhrase.Value;

            if (StudyStage == StudyStages.ReversedQuiz)
                _userAnswers.Single(x => x.PhraseUsage.Id == CurrentPhrase.Id).IsRight = isRight;
            else
                _userAnswers.Add(new UserAnswerDTO(Guid.NewGuid(), CurrentPhrase, isRight));

            var offset = StudyStage == StudyStages.OriginalQuiz ? 
                _originalPhrases.Count : _originalPhrases.Count * 2;
            StudyProgress = (double)(_currentPhraseIndex + offset + 1) / maxProgress;
        }

        // finish lesson
        if (StudyStage == StudyStages.ReversedQuiz && _currentPhraseIndex + 1 == _originalPhrases.Count)
        {
            var correctAnswers = _userAnswers.Count(x => x.IsRight);
            var wrongAnswers = _userAnswers.Count - correctAnswers;

            await sender.Send(new AddUserAnswersRequest(_userAnswers));
            
            await Shell.Current.DisplayAlert(AppResources.Attention,
                $"Lesson finished. Correct answers: {correctAnswers}, wrong answers: {wrongAnswers}", AppResources.Ok);
            
            await Shell.Current.GoBack();
            
            return;
        }
        
        // update stage
        if (StudyStage == StudyStages.OriginalQuiz && _currentPhraseIndex + 1 == _originalPhrases.Count)
        {
            _currentPhraseIndex = -1;
            StudyStage = StudyStages.ReversedQuiz;
        }
        else if (_currentPhraseIndex + 1 == _originalPhrases.Count)
        {
            _currentPhraseIndex = -1;
            StudyStage = StudyStages.OriginalQuiz;
        }

        // set CurrentPhrase
        _currentPhraseIndex++;
        
        CurrentPhrase = StudyStage == StudyStages.ReversedQuiz ? 
            _reversedPhrasess[_currentPhraseIndex] : _originalPhrases[_currentPhraseIndex];
        
        if (StudyStage == StudyStages.Learning)
            return;

        // on quiz stage fill answers with new values
        var indexes = new List<int>(Answers.Length);
        
        var phraseUsages = StudyStage == StudyStages.ReversedQuiz ? 
            _reversedPhrasess : _originalPhrases;
        // exclude correct answer
        var phrases = phraseUsages.Where(x => x.Id != CurrentPhrase.Id)
            .Select(x => x.TranslationPhrase.Value).ToArray();
        var i = 0;
        
        // set SelectedAnswer and Answers empty
        SelectedAnswer = "_";
        for (var j = 0; j < Answers.Length; j++)
            Answers[j] = j + "";
        OnPropertyChanged(nameof(Answers));
        
        while (indexes.Count != Answers.Length)
        {
            var index = _random.Next(0, phrases.Length);
            if (indexes.Contains(index))
                continue;

            indexes.Add(index);
            Answers[i] = phrases[index];
            i++;
        }
        
        // replace random phrase to correct answer
        var correctAnswerIndex = _random.Next(Answers.Length);
        Answers[correctAnswerIndex] = CurrentPhrase.TranslationPhrase.Value;
        
        // force update for notify property changed
        OnPropertyChanged(nameof(Answers));

        SelectedAnswer = Answers.First();
    }
    
}