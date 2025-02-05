using System.Globalization;
using MediatR;
using MyVocabulary.Application.Models;

namespace MyVocabulary.Application.Queries.Languages.Handlers;

internal class GetLanguagesHandler : IRequestHandler<GetLanguagesRequest, Language[]>
{
    public async Task<Language[]> Handle(GetLanguagesRequest request, CancellationToken cancellationToken)
    {
        var languages = CultureInfo.GetCultures(CultureTypes.NeutralCultures)
            .Where(x => !string.IsNullOrWhiteSpace(x.Name))
            .Select(x => new Language(x))
            .OrderBy(x => x.Value != "en")
            .ThenBy(x => x.Value)
            .ToArray();

        return await Task.FromResult(languages);
    }
}