using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using MyVocabulary.Application.Commands.PhraseUsages;
using MyVocabulary.Application.Models;
using MyVocabulary.UI.Enums;
using MyVocabulary.UI.Extensions;
using MyVocabulary.UI.Localization;
using MyVocabulary.UI.NavigationParameters;

namespace MyVocabulary.UI.PageModels;

public partial class PhraseUsageDetailPageModel(ISender sender) : ObservableObject, IQueryAttributable
{
    
    private TopicDTO _topic = null!;

    [ObservableProperty]
    private PhraseUsageDTO _wordUsage = null!;

    [ObservableProperty]
    private ObservableCollection<string> _languages = [];

    [ObservableProperty]
    private string? _pictureUrl;

    [ObservableProperty]
    private bool _isCreateMode;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var parameters = PhraseUsageNavigationParameter.From(query);
        _topic = parameters.Topic!;

        if (parameters.NavigationType == NavigationTypes.Open)
        {
            WordUsage = parameters.PhraseUsage;
        }
        else
        {
            IsCreateMode = true;
            WordUsage = new PhraseUsageDTO(Guid.NewGuid(), 
                _topic, 
                new PhraseDTO("", _topic.CultureFrom),
                new PhraseDTO("", _topic.CultureTo), 
                "", "", "");
        }
        PictureUrl = WordUsage.PhotoUrl;
    }

    [RelayCommand]
    private async Task ChangeImage()
    {
        var imageUrl = await Shell.Current.ShowPopupAsync(new Controls.ChooseImagePopup(PictureUrl)) as string;
        PictureUrl = imageUrl;
    }

    [RelayCommand]
    private async Task SaveWordUsage()
    {
        var ensure = await Shell.Current.DisplayAlert(AppResources.Attention,
            "Are you sure that you want to save this phrase usage ?", AppResources.Yes, AppResources.No);

        if (!ensure)
            return;

        WordUsage.PhotoUrl = PictureUrl;

        if (IsCreateMode)
        {
            var result = await sender.Send(new AddPhraseUsageRequest(WordUsage));

            if (!result.IsSuccess)
            {
                var message = Environment.NewLine + string.Join(";" + Environment.NewLine, result.Errors);
                await Shell.Current.DisplayAlert(AppResources.Error, $"You can't create topic: {message}", AppResources.Ok);
                return;
            }

            await Toast.Make("Phrase usage successfully created").Show();

            WordUsage.Id = result.Value.Id;
            IsCreateMode = false;
        }
        else
        {
            var result = await sender.Send(new EditPhraseUsageRequest(WordUsage));

            if (!result.IsSuccess)
            {
                var message = Environment.NewLine + string.Join(";" + Environment.NewLine, result.Errors);
                await Shell.Current.DisplayAlert(AppResources.Error, $"You can't edit topic: {message}", AppResources.Ok);
                return;
            }

            await Toast.Make("Phrase usage successfully edited").Show();
        }
    }

    [RelayCommand]
    private async Task DeleteWordUsage()
    {
        var ensure = await Shell.Current.DisplayAlert(AppResources.Attention,
            "Are you sure that you want to delete this phrase usage ?", AppResources.Yes, AppResources.No);

        if (!ensure)
            return;

        if (IsCreateMode)
        {
            await Shell.Current.GoBack();
            return;
        }

        await sender.Send(new DeletePhraseUsageRequest(WordUsage.Id));
        await Toast.Make("Phrase usage successfully deleted").Show();
        await Shell.Current.GoBack();
    }

}