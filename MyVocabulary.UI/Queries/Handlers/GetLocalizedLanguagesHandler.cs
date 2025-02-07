using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.Languages;
using MyVocabulary.UI.Localization;

namespace MyVocabulary.UI.Queries.Handlers;

internal class GetLocalizedLanguagesHandler : IRequestHandler<GetLocalizedLanguagesRequest, Language[]>
{

    private readonly ISender _sender;

    public GetLocalizedLanguagesHandler(ISender sender)
    {
        _sender = sender;
    }

    public async Task<Language[]> Handle(GetLocalizedLanguagesRequest request, CancellationToken cancellationToken)
    {
        var languages = await _sender.Send(new GetLanguagesRequest());
        return languages
            .Where(x => AppResources.ResourceManager.GetResourceSet(x.Culture, false, false) != null)
            .ToArray();
    }

}