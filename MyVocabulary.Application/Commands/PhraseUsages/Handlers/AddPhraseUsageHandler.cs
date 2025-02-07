using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Commands.Phrases;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.PhraseUsages;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Commands.PhraseUsages.Handlers;

internal class AddPhraseUsageHandler : IRequestHandler<AddPhraseUsageRequest, Result<PhraseUsageDTO>>
{

    private readonly IRepository<PhraseUsage> _phraseUsagesRepository;
    private readonly ISender _sender;

    public AddPhraseUsageHandler(IRepository<PhraseUsage> phraseUsagesRepository, ISender sender)
    {
        _phraseUsagesRepository = phraseUsagesRepository;
        _sender = sender;
    }

    public async Task<Result<PhraseUsageDTO>> Handle(AddPhraseUsageRequest request, CancellationToken cancellationToken)
    {
        var nativePhrase = await _sender.Send(new GetOrCreatePhraseRequest(
            request.Entity.NativePhrase.Value, request.Entity.NativePhrase.Language));
        var translationPhrase = await _sender.Send(new GetOrCreatePhraseRequest(
            request.Entity.TranslationPhrase.Value, request.Entity.TranslationPhrase.Language));

        var phraseUsage = new PhraseUsage(request.Entity.Topic.Id,
            nativePhrase.Value.Id,
            translationPhrase.Value.Id,
            request.Entity.NativeSentence, 
            request.Entity.TranslatedSentence, 
            request.Entity.PhotoUrl);

        var result = await _phraseUsagesRepository.AddAsync(phraseUsage);
        var phraseUsageDTO = await _sender.Send(new GetPhraseUsageRequest(result.Id));

        return phraseUsageDTO;
    }

}