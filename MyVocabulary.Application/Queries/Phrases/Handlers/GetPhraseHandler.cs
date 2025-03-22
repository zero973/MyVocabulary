using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Queries.Phrases.Handlers;

internal class GetPhraseHandler(IRepository<Phrase> phrasesRepository)
    : IRequestHandler<GetPhraseRequest, Result<PhraseDTO>>
{
    public async Task<Result<PhraseDTO>> Handle(GetPhraseRequest request, CancellationToken cancellationToken)
    {
        var phrase = await phrasesRepository.FirstOrDefaultAsync(request.Specification, cancellationToken);
        if (phrase == null)
            return Result.NotFound("Didn't find phrase with such specification");
        return new PhraseDTO(phrase.Id, phrase.Value, new Language(phrase.Culture));
    }

}