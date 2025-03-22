using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Queries.Phrases.Handlers;

internal class GetPhrasesHandler(IRepository<Phrase> phrasesRepository)
    : IRequestHandler<GetPhrasesRequest, Result<List<PhraseDTO>>>
{
    public async Task<Result<List<PhraseDTO>>> Handle(GetPhrasesRequest request, CancellationToken cancellationToken)
    {
        var phrases = await phrasesRepository.ListAsync(request.Specification, cancellationToken);
        return phrases.Select(x => new PhraseDTO(x.Id, x.Value, new Language(x.Culture))).ToList();
    }

}