using Ardalis.Result;
using MediatR;
using Microsoft.Maui.Platform;
using MyVocabulary.Application.Commands.App;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.Languages;

namespace MyVocabulary.UI.Queries.Handlers;

internal class LoadUserSettingsHandler(ISender sender) : IRequestHandler<LoadUserSettingsRequest, Result<UserSettings>>
{
    public async Task<Result<UserSettings>> Handle(LoadUserSettingsRequest request, CancellationToken cancellationToken)
    {
        var languages = await sender.Send(new GetLanguagesRequest(), cancellationToken);

        var pref = Preferences.Default;
        var appLanguage = new Language(pref.Get(nameof(UserSettings.AppLanguage), "en"));
        var preferredLanguages = pref.Get(nameof(UserSettings.PreferredLanguages), "en")
            .Split(";")
            .Select(x => new Language(x))
            // take only valid languages
            .Where(x => languages.Any(l => l.Equals(x)))
            .ToArray();
        var countMonthsValidAnswers = pref.Get(nameof(UserSettings.CountMonthsValidAnswers), 1);

        return new UserSettings(appLanguage, preferredLanguages, (uint)countMonthsValidAnswers);
    }
}