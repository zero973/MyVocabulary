using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Commands.Phrases.Handlers;

internal class EditPhraseHandler(IRepository<Phrase> phrasesRepository)
    : IRequestHandler<EditPhraseRequest, Result<PhraseDTO>>
{
    public async Task<Result<PhraseDTO>> Handle(EditPhraseRequest request, CancellationToken cancellationToken)
    {
        var phrase = await phrasesRepository.GetByIdAsync(request.Entity.Id, cancellationToken);
        phrase!.Edit(request.Entity.Value, request.Entity.Language.Value);

        await phrasesRepository.UpdateAsync(phrase, cancellationToken);

        return new PhraseDTO(phrase.Id, phrase.Value, new Language(phrase.Culture));
    }
}