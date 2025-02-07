using Ardalis.Result;
using MediatR;
using Microsoft.Maui.Platform;
using MyVocabulary.Application.Commands.App;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.Languages;

namespace MyVocabulary.UI.Queries.Handlers;

internal class LoadUserSettingsHandler : IRequestHandler<LoadUserSettingsRequest, Result<UserSettings>>
{

    private readonly ISender _sender;

    public LoadUserSettingsHandler(ISender sender)
    {
        _sender = sender;
    }

    public async Task<Result<UserSettings>> Handle(LoadUserSettingsRequest request, CancellationToken cancellationToken)
    {
        var languages = await _sender.Send(new GetLanguagesRequest());

        var pref = Preferences.Default;
        var appLanguage = new Language(pref.Get(nameof(UserSettings.AppLanguage), "en"));
        var preferredLanguages = pref.Get(nameof(UserSettings.PreferredLanguages), "en")
            .Split(";", StringSplitOptions.None)
            .Select(x => new Language(x))
            // take only valid languages
            .Where(x => languages.Any(l => l.Equals(x)))
            .ToArray();

        return new UserSettings(appLanguage, preferredLanguages);
    }
}