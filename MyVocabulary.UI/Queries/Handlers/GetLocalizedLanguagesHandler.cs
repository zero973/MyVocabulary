using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.Languages;
using MyVocabulary.UI.Localization;

namespace MyVocabulary.UI.Queries.Handlers;

internal class GetLocalizedLanguagesHandler(ISender sender) : IRequestHandler<GetLocalizedLanguagesRequest, Language[]>
{
    public async Task<Language[]> Handle(GetLocalizedLanguagesRequest request, CancellationToken cancellationToken)
    {
        var languages = await sender.Send(new GetLanguagesRequest(), cancellationToken);
        return languages
            .Where(x => AppResources.ResourceManager.GetResourceSet(x.Culture, false, false) != null)
            .ToArray();
    }
}