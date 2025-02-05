using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.Words;
using MyVocabulary.Application.Specifications;

namespace MyVocabulary.UI.PageModels;

public partial class WordsPageModel(ISender _sender) : ObservableObject
{

    [ObservableProperty]
    private ObservableCollection<WordDTO> _words = [];

    [RelayCommand]
    private async Task Appearing()
    {
        await LoadData();
    }

    private async Task LoadData()
    {
        var words = await _sender.Send(new GetWordsRequest(new WordsSpecification(0, 10)));
        Words = new ObservableCollection<WordDTO>(words);
    }

}