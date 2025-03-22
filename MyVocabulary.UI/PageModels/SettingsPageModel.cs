using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using MyVocabulary.Application.Commands.App;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.Languages;
using MyVocabulary.UI.Localization;

namespace MyVocabulary.UI.PageModels;

public partial class SettingsPageModel(ISender sender) : ObservableObject
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

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CuntMonthsValidAnswersText))]
    private uint _countMonthsValidAnswers;

    public string CuntMonthsValidAnswersText => $"Count months valid answers: {CountMonthsValidAnswers}";

    [RelayCommand]
    private async Task Appearing()
    {
        var languages = await sender.Send(new GetLanguagesRequest());
        _languagesDict = languages.ToDictionary(x => x.Name, x => x);
        Languages = new ObservableCollection<string>(_languagesDict.Keys);

        var result = await sender.Send(new LoadUserSettingsRequest());

        SelectedAppLanguage = result.Value.AppLanguage.Name;
        SelectedPreferLanguage = Languages[0];
        PreferredLanguages = new ObservableCollection<Language>(result.Value.PreferredLanguages);
        CountMonthsValidAnswers = result.Value.CountMonthsValidAnswers;
    }

    [RelayCommand]
    private async Task AddPreferLanguage()
    {
        if (PreferredLanguages.All(x => x.Name != SelectedPreferLanguage))
            PreferredLanguages.Add(_languagesDict[SelectedPreferLanguage]);
        else
            await Toast.Make("This language already added in list").Show();
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
        var result = await sender.Send(new SaveUserSettingsRequest(new UserSettings(
            _languagesDict[SelectedAppLanguage], PreferredLanguages.ToArray(), CountMonthsValidAnswers)));

        if (result.IsSuccess)
        {
            await Toast.Make("Settings saved successfully").Show();
            return;
        }

        var message = Environment.NewLine + string.Join(";" + Environment.NewLine, result.Errors);
        await Shell.Current.DisplayAlert(AppResources.Error, 
            $"An error occurred while saving the changes: {message}", AppResources.Ok);
    }

}