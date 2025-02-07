using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
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

public partial class PhraseUsageDetailPageModel(ISender _sender) : ObservableObject, IQueryAttributable
{

    // todo replace it somewhere
    private const string NoImageUrl = "https://sun9-58.userapi.com/impg/T2LyUzjz8C3DKtVoI7p6fo2edXNP04AOcYsDZQ/2Pj8k46yrqk.jpg?size=383x341&quality=95&sign=3f0a715c29c549d26b93b073a344df45";

    private TopicDTO _topic = null!;

    [ObservableProperty]
    private PhraseUsageDTO _wordUsage = null!;

    [ObservableProperty]
    private ObservableCollection<string> _languages = [];

    [ObservableProperty]
    private string? _pictureUrl;

    [ObservableProperty]
    public bool _isCreateMode = false;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var parameters = PhraseUsageNavigationParameter.From(query);
        _topic = parameters.Topic!;

        if (parameters.Mode == NavigationModes.Exists)
        {
            WordUsage = parameters.Value!;
        }
        else
        {
            IsCreateMode = true;
            WordUsage = new PhraseUsageDTO(Guid.NewGuid(), 
                _topic, 
                new PhraseDTO("", _topic.CultureFrom),
                new PhraseDTO("", _topic.CultureTo), 
                "", "", NoImageUrl);
        }
        PictureUrl = WordUsage.PhotoUrl;
    }

    [RelayCommand]
    private async Task Appearing()
    {
        await LoadData();
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
            var result = await _sender.Send(new AddPhraseUsageRequest(WordUsage));

            if (!result.IsSuccess)
            {
                var message = Environment.NewLine + string.Join(";" + Environment.NewLine, result.Errors);
                await Shell.Current.DisplayAlert(AppResources.Error, $"You can't create topic: {message}", AppResources.Ok);
                return;
            }

            await Toast.Make("Phrase usage successfully created", ToastDuration.Short).Show();

            WordUsage.Id = result.Value.Id;
            IsCreateMode = false;
        }
        else
        {
            var result = await _sender.Send(new EditPhraseUsageRequest(WordUsage));

            if (!result.IsSuccess)
            {
                var message = Environment.NewLine + string.Join(";" + Environment.NewLine, result.Errors);
                await Shell.Current.DisplayAlert(AppResources.Error, $"You can't edit topic: {message}", AppResources.Ok);
                return;
            }

            await Toast.Make("Phrase usage successfully edited", ToastDuration.Short).Show();
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

        await _sender.Send(new DeletePhraseUsageRequest(WordUsage.Id));
        await Toast.Make("Phrase usage successfully deleted", ToastDuration.Short).Show();
        await Shell.Current.GoBack();
    }

    private async Task LoadData()
    {
        
    }

}