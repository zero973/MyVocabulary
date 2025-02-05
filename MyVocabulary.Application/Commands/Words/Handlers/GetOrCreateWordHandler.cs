using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.Words;
using MyVocabulary.Application.Specifications;

namespace MyVocabulary.Application.Commands.Words.Handlers;

internal class GetOrCreateWordHandler : IRequestHandler<GetOrCreateWordRequest, Result<WordDTO>>
{

    private readonly ISender _sender;

    public GetOrCreateWordHandler(ISender sender)
    {
        _sender = sender;
    }

    public async Task<Result<WordDTO>> Handle(GetOrCreateWordRequest request, CancellationToken cancellationToken)
    {
        var wordResult = await _sender.Send(new GetWordRequest(
            new WordSpecification(request.Word, request.Language)));

        // if we didn't find word, then we will add it
        if (!wordResult.IsSuccess)
        {
            return await _sender.Send(new AddWordRequest(
                new WordDTO(request.Word, request.Language)));
        }

        return wordResult;
    }

}