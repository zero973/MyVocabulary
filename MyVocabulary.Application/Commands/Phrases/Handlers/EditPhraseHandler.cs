using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Commands.Phrases.Handlers;

internal class EditPhraseHandler : IRequestHandler<EditPhraseRequest, Result<PhraseDTO>>
{

    private readonly IRepository<Phrase> _phrasesRepository;

    public EditPhraseHandler(IRepository<Phrase> phrasesRepository)
    {
        _phrasesRepository = phrasesRepository;
    }

    public async Task<Result<PhraseDTO>> Handle(EditPhraseRequest request, CancellationToken cancellationToken)
    {
        var phrase = await _phrasesRepository.GetByIdAsync(request.Entity.Id);
        phrase!.Edit(request.Entity.Value, request.Entity.Language.Value);

        await _phrasesRepository.UpdateAsync(phrase);

        return new PhraseDTO(phrase.Id, phrase.Value, new Language(phrase.Culture));
    }

}