using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Commands.Phrases;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.PhraseUsages;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Commands.PhraseUsages.Handlers;

internal class AddPhraseUsageHandler(IRepository<PhraseUsage> phraseUsagesRepository, ISender sender)
    : IRequestHandler<AddPhraseUsageRequest, Result<PhraseUsageDTO>>
{
    public async Task<Result<PhraseUsageDTO>> Handle(AddPhraseUsageRequest request, CancellationToken cancellationToken)
    {
        var nativePhrase = await sender.Send(new GetOrCreatePhraseRequest(
            request.Entity.NativePhrase.Value, request.Entity.NativePhrase.Language), cancellationToken);
        var translationPhrase = await sender.Send(new GetOrCreatePhraseRequest(
            request.Entity.TranslationPhrase.Value, request.Entity.TranslationPhrase.Language), cancellationToken);

        var phraseUsage = new PhraseUsage(request.Entity.Topic.Id,
            nativePhrase.Value.Id,
            translationPhrase.Value.Id,
            request.Entity.NativeSentence, 
            request.Entity.TranslatedSentence, 
            request.Entity.PhotoUrl);

        var result = await phraseUsagesRepository.AddAsync(phraseUsage, cancellationToken);
        var phraseUsageDto = await sender.Send(new GetPhraseUsageRequest(result.Id), cancellationToken);

        return phraseUsageDto;
    }
}