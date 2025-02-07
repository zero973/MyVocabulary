namespace MyVocabulary.Application.Models;

public class UserSettings
{

    public Language AppLanguage { get; set; }

    public Language[] PreferredLanguages { get; set; }

    public UserSettings(Language appLanguage, Language[] preferredLanguages)
    {
        AppLanguage = appLanguage;
        PreferredLanguages = preferredLanguages;
    }

}