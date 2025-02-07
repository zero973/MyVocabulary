using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Commands.App;
using MyVocabulary.Application.Models;

namespace MyVocabulary.UI.Commands.Handlers;

internal class SaveUserSettingsHandler : IRequestHandler<SaveUserSettingsRequest, Result>
{
    public async Task<Result> Handle(SaveUserSettingsRequest request, CancellationToken cancellationToken)
    {
        var pref = Preferences.Default;

        var languages = request.UserSettings.PreferredLanguages.Select(x => x.Value);

        pref.Set(nameof(UserSettings.AppLanguage), request.UserSettings.AppLanguage.Value);
        pref.Set(nameof(UserSettings.PreferredLanguages), string.Join(";", languages));

        return Result.Success();
    }
}