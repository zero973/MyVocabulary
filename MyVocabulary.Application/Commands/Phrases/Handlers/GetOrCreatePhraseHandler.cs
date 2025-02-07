using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.Phrases;
using MyVocabulary.Application.Specifications;

namespace MyVocabulary.Application.Commands.Phrases.Handlers;

internal class GetOrCreatePhraseHandler : IRequestHandler<GetOrCreatePhraseRequest, Result<PhraseDTO>>
{

    private readonly ISender _sender;

    public GetOrCreatePhraseHandler(ISender sender)
    {
        _sender = sender;
    }

    public async Task<Result<PhraseDTO>> Handle(GetOrCreatePhraseRequest request, CancellationToken cancellationToken)
    {
        var phraseResult = await _sender.Send(new GetPhraseRequest(
            new PhraseSpecification(request.Phrase, request.Language)));

        // if we didn't find phrase, then we will add it
        if (!phraseResult.IsSuccess)
        {
            return await _sender.Send(new AddPhraseRequest(
                new PhraseDTO(request.Phrase, request.Language)));
        }

        return phraseResult;
    }

}