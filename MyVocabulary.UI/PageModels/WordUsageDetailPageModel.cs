using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using MyVocabulary.Application.Commands.WordUsages;
using MyVocabulary.Application.Models;
using MyVocabulary.UI.Enums;
using MyVocabulary.UI.Extensions;
using MyVocabulary.UI.Localization;
using MyVocabulary.UI.NavigationParameters;

namespace MyVocabulary.UI.PageModels;

public partial class WordUsageDetailPageModel(ISender _sender) : ObservableObject, IQueryAttributable
{

    // todo replace it somewhere
    private const string NoImageUrl = "https://sun9-58.userapi.com/impg/T2LyUzjz8C3DKtVoI7p6fo2edXNP04AOcYsDZQ/2Pj8k46yrqk.jpg?size=383x341&quality=95&sign=3f0a715c29c549d26b93b073a344df45";

    private TopicDTO _topic = null!;

    [ObservableProperty]
    private WordUsageDTO _wordUsage = null!;

    [ObservableProperty]
    private ObservableCollection<string> _languages = [];

    [ObservableProperty]
    private string? _pictureUrl;

    [ObservableProperty]
    public bool _isCreateMode = false;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var parameters = WordUsageNavigationParameter.From(query);
        _topic = parameters.Topic!;

        if (parameters.Mode == NavigationModes.Exists)
        {
            WordUsage = parameters.Value!;
        }
        else
        {
            IsCreateMode = true;
            WordUsage = new WordUsageDTO(Guid.NewGuid(), 
                _topic, 
                new WordDTO("", _topic.CultureFrom),
                new WordDTO("", _topic.CultureTo), 
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
            "Are you sure that you want to save this word usage ?", AppResources.Yes, AppResources.No);

        if (!ensure)
            return;

        WordUsage.PhotoUrl = PictureUrl;

        if (IsCreateMode)
        {
            var result = await _sender.Send(new AddWordUsageRequest(WordUsage));

            if (!result.IsSuccess)
            {
                var message = Environment.NewLine + string.Join(";" + Environment.NewLine, result.Errors);
                await Shell.Current.DisplayAlert(AppResources.Error, $"You can't create topic: {message}", AppResources.Ok);
                return;
            }

            await Toast.Make("Word usage successfully created", ToastDuration.Short).Show();

            WordUsage.Id = result.Value.Id;
            IsCreateMode = false;
        }
        else
        {
            var result = await _sender.Send(new EditWordUsageRequest(WordUsage));

            if (!result.IsSuccess)
            {
                var message = Environment.NewLine + string.Join(";" + Environment.NewLine, result.Errors);
                await Shell.Current.DisplayAlert(AppResources.Error, $"You can't edit topic: {message}", AppResources.Ok);
                return;
            }

            await Toast.Make("Word usage successfully edited", ToastDuration.Short).Show();
        }
    }

    [RelayCommand]
    private async Task DeleteWordUsage()
    {
        var ensure = await Shell.Current.DisplayAlert(AppResources.Attention,
            "Are you sure that you want to delete this word usage ?", AppResources.Yes, AppResources.No);

        if (!ensure)
            return;

        if (IsCreateMode)
        {
            await Shell.Current.GoBack();
            return;
        }

        await _sender.Send(new DeleteWordUsageRequest(WordUsage.Id));
        await Toast.Make("Word usage successfully deleted", ToastDuration.Short).Show();
        await Shell.Current.GoBack();
    }

    private async Task LoadData()
    {
        
    }

}