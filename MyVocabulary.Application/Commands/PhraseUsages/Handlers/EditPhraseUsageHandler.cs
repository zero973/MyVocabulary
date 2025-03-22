using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Commands.Phrases;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.PhraseUsages;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Commands.PhraseUsages.Handlers;

internal class EditPhraseUsageHandler(IRepository<PhraseUsage> phraseUsagesRepository, ISender sender)
    : IRequestHandler<EditPhraseUsageRequest, Result<PhraseUsageDTO>>
{
    public async Task<Result<PhraseUsageDTO>> Handle(EditPhraseUsageRequest request, CancellationToken cancellationToken)
    {
        var phraseUsage = await phraseUsagesRepository.GetByIdAsync(request.Entity.Id);
        var nativePhrase = await sender.Send(new GetOrCreatePhraseRequest(
             request.Entity.NativePhrase.Value, request.Entity.NativePhrase.Language));
        var translationPhrase = await sender.Send(new GetOrCreatePhraseRequest(
            request.Entity.TranslationPhrase.Value, request.Entity.TranslationPhrase.Language));

        phraseUsage!.Edit(request.Entity.Topic.Id,
            nativePhrase.Value.Id,
            translationPhrase.Value.Id,
            request.Entity.NativeSentence,
            request.Entity.TranslatedSentence,
            request.Entity.PhotoUrl);

        await phraseUsagesRepository.UpdateAsync(phraseUsage);
        var phraseUsageDto = await sender.Send(new GetPhraseUsageRequest(phraseUsage.Id));

        return phraseUsageDto;
    }
}