using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Commands.Phrases.Handlers;

internal class AddPhraseHandler : IRequestHandler<AddPhraseRequest, Result<PhraseDTO>>
{

    private readonly IRepository<Phrase> _phrasesRepository;

    public AddPhraseHandler(IRepository<Phrase> phrasesRepository)
    {
        _phrasesRepository = phrasesRepository;
    }

    public async Task<Result<PhraseDTO>> Handle(AddPhraseRequest request, CancellationToken cancellationToken)
    {
        var phrase = await _phrasesRepository.AddAsync(
            new Phrase(request.Entity.Value, request.Entity.Language.Value));

        return new PhraseDTO(phrase.Id, phrase.Value, new Language(phrase.Culture));
    }

}