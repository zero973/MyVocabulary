using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.Topics;
using MyVocabulary.Application.Queries.Phrases;
using MyVocabulary.Application.Specifications;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Queries.PhraseUsages.Handlers;

internal class GetPhraseUsageHandler(IRepository<PhraseUsage> phraseUsagesRepository, ISender sender)
    : IRequestHandler<GetPhraseUsageRequest, Result<PhraseUsageDTO>>
{
    public async Task<Result<PhraseUsageDTO>> Handle(GetPhraseUsageRequest request, CancellationToken cancellationToken)
    {
        var phraseUsage = (await phraseUsagesRepository.GetByIdAsync(request.Id, cancellationToken))!;
        var topic = await sender.Send(new GetTopicRequest(phraseUsage.TopicId), cancellationToken);
        var phrases = (await sender.Send(new GetPhrasesRequest(
                new PhrasesSpecification(phraseUsage.NativePhraseId, phraseUsage.TranslationPhraseId)), cancellationToken))
            .Value.ToDictionary(x => x.Id, x => x);

        return new PhraseUsageDTO(phraseUsage.Id, 
            topic, 
            phrases[phraseUsage.NativePhraseId], 
            phrases[phraseUsage.TranslationPhraseId], 
            phraseUsage.NativeSentence, 
            phraseUsage.TranslatedSentence, 
            phraseUsage.PhotoUrl);
    }
}