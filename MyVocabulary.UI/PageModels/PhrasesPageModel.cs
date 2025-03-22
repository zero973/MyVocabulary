using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.Phrases;
using MyVocabulary.Application.Specifications;

namespace MyVocabulary.UI.PageModels;

public partial class PhrasesPageModel(ISender sender) : ObservableObject
{

    [ObservableProperty]
    private ObservableCollection<PhraseDTO> _phrases = [];

    [RelayCommand]
    private async Task Appearing()
    {
        await LoadData();
    }

    private async Task LoadData()
    {
        var words = await sender.Send(new GetPhrasesRequest(new PhrasesSpecification(0, 10)));
        Phrases = new ObservableCollection<PhraseDTO>(words);
    }

}