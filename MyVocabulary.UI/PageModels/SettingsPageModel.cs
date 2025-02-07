using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using MyVocabulary.Application.Commands.App;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.Languages;
using MyVocabulary.UI.Localization;

namespace MyVocabulary.UI.PageModels;

public partial class SettingsPageModel(ISender _sender) : ObservableObject
{

    /// <summary>
    /// Languages dictionary
    /// </summary>
    private Dictionary<string, Language> _languagesDict = null!;

    [ObservableProperty]
    private ObservableCollection<string> _languages = [];

    [ObservableProperty]
    private string _selectedAppLanguage = null!;

    [ObservableProperty]
    private string _selectedPreferLanguage = null!;

    [ObservableProperty]
    private ObservableCollection<Language> _preferredLanguages = [];

    [RelayCommand]
    private async Task Appearing()
    {
        var languages = await _sender.Send(new GetLanguagesRequest());
        _languagesDict = languages.ToDictionary(x => x.Name, x => x);
        Languages = new ObservableCollection<string>(_languagesDict.Keys);

        var result = await _sender.Send(new LoadUserSettingsRequest());

        SelectedAppLanguage = result.Value.AppLanguage.Name;
        SelectedPreferLanguage = Languages[0];
        PreferredLanguages = new ObservableCollection<Language>(result.Value.PreferredLanguages);
    }

    [RelayCommand]
    private async Task AddPreferLanguage()
    {
        if (!PreferredLanguages.Any(x => x.Name == SelectedPreferLanguage))
            PreferredLanguages.Add(_languagesDict[SelectedPreferLanguage]);
        else
            await Toast.Make("This language already added in list", ToastDuration.Short).Show();
    }

    [RelayCommand]
    private async Task Tap(Language language)
    {
        var ensure = await Shell.Current.DisplayAlert(AppResources.Attention,
            $"Are you sure that you want to delete \"{language.Name}\" language ?", AppResources.Yes, AppResources.No);

        if (!ensure)
            return;

        PreferredLanguages.Remove(language);
    }

    [RelayCommand]
    private async Task Save()
    {
        var result = await _sender.Send(new SaveUserSettingsRequest(new UserSettings(
            _languagesDict[SelectedAppLanguage], PreferredLanguages.ToArray())));

        if (result.IsSuccess)
        {
            await Toast.Make("Settings saved successfully", ToastDuration.Short).Show();
            return;
        }

        var message = Environment.NewLine + string.Join(";" + Environment.NewLine, result.Errors);
        await Shell.Current.DisplayAlert(AppResources.Error, 
            $"An error occurred while saving the changes: {message}", AppResources.Ok);
    }

}