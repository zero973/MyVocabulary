using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.Phrases;
using MyVocabulary.Application.Specifications;

namespace MyVocabulary.Application.Commands.Phrases.Handlers;

internal class GetOrCreatePhraseHandler(ISender sender) : IRequestHandler<GetOrCreatePhraseRequest, Result<PhraseDTO>>
{
    public async Task<Result<PhraseDTO>> Handle(GetOrCreatePhraseRequest request, CancellationToken cancellationToken)
    {
        var phraseResult = await sender.Send(new GetPhraseRequest(
            new PhraseSpecification(request.Phrase, request.Language)), cancellationToken);

        // if we didn't find phrase, then we will add it
        if (!phraseResult.IsSuccess)
        {
            return await sender.Send(new AddPhraseRequest(
                new PhraseDTO(request.Phrase, request.Language)), cancellationToken);
        }

        return phraseResult;
    }
}