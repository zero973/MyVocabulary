using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Queries.Phrases.Handlers;

internal class GetPhraseHandler : IRequestHandler<GetPhraseRequest, Result<PhraseDTO>>
{

    private readonly IRepository<Phrase> _phrasesRepository;

    public GetPhraseHandler(IRepository<Phrase> phrasesRepository)
    {
        _phrasesRepository = phrasesRepository;
    }

    public async Task<Result<PhraseDTO>> Handle(GetPhraseRequest request, CancellationToken cancellationToken)
    {
        var phrase = await _phrasesRepository.FirstOrDefaultAsync(request.Specification);
        if (phrase == default)
            return Result.NotFound("Didn't find phrase with such specification");
        return new PhraseDTO(phrase.Id, phrase.Value, new Language(phrase.Culture));
    }

}