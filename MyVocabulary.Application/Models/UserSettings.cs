namespace MyVocabulary.Application.Models;

public class UserSettings
{

    public Language AppLanguage { get; set; }

    public Language[] PreferredLanguages { get; set; }
    
    public uint CountMonthsValidAnswers { get; set; }

    public UserSettings(Language appLanguage, Language[] preferredLanguages, uint countMonthsValidAnswers)
    {
        AppLanguage = appLanguage;
        PreferredLanguages = preferredLanguages;
        CountMonthsValidAnswers = countMonthsValidAnswers;
    }

}