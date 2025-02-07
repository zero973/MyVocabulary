using System.Globalization;
using MediatR;
using MyVocabulary.Application.Commands.App;
using MyVocabulary.Application.Models;

namespace MyVocabulary.Application.Queries.Languages.Handlers;

internal class GetSortedLanguagesHandler : IRequestHandler<GetSortedLanguagesRequest, Language[]>
{

    private readonly ISender _sender;

    public GetSortedLanguagesHandler(ISender sender)
    {
        _sender = sender;
    }

    public async Task<Language[]> Handle(GetSortedLanguagesRequest request, CancellationToken cancellationToken)
    {
        var preferredLanguages = (await _sender.Send(new LoadUserSettingsRequest())).Value.PreferredLanguages;

        var languages = CultureInfo.GetCultures(CultureTypes.NeutralCultures)
            .Where(x => !string.IsNullOrWhiteSpace(x.Name))
            .Select(x => new Language(x))
            .OrderBy(x => !preferredLanguages.Any(l => l.Equals(x)))
            .ThenBy(x => x.Value)
            .ToArray();

        return await Task.FromResult(languages);
    }
}