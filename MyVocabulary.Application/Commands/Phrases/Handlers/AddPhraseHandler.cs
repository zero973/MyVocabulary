using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Commands.Phrases.Handlers;

internal class AddPhraseHandler(IRepository<Phrase> phrasesRepository)
    : IRequestHandler<AddPhraseRequest, Result<PhraseDTO>>
{
    public async Task<Result<PhraseDTO>> Handle(AddPhraseRequest request, CancellationToken cancellationToken)
    {
        var phrase = await phrasesRepository.AddAsync(
            new Phrase(request.Entity.Value, request.Entity.Language.Value), cancellationToken);

        return new PhraseDTO(phrase.Id, phrase.Value, new Language(phrase.Culture));
    }
}