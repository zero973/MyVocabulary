using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Queries.Phrases.Handlers;

internal class GetPhrasesHandler : IRequestHandler<GetPhrasesRequest, Result<List<PhraseDTO>>>
{

    private readonly IRepository<Phrase> _phrasesRepository;

    public GetPhrasesHandler(IRepository<Phrase> phrasesRepository)
    {
        _phrasesRepository = phrasesRepository;
    }

    public async Task<Result<List<PhraseDTO>>> Handle(GetPhrasesRequest request, CancellationToken cancellationToken)
    {
        var phrases = await _phrasesRepository.ListAsync(request.Specification);
        return phrases.Select(x => new PhraseDTO(x.Id, x.Value, new Language(x.Culture))).ToList();
    }

}