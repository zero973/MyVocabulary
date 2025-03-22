using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.Topics;
using MyVocabulary.Application.Queries.Phrases;
using MyVocabulary.Application.Specifications;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Queries.PhraseUsages.Handlers;

internal class GetPhraseUsagesHandler(
    IRepository<PhraseUsage> phraseUsagesRepository,
    ISender sender)
    : IRequestHandler<GetPhraseUsagesRequest, Result<List<PhraseUsageDTO>>>
{
    public async Task<Result<List<PhraseUsageDTO>>> Handle(GetPhraseUsagesRequest request, CancellationToken cancellationToken)
    {
        var phraseUsages = await phraseUsagesRepository.ListAsync(request.Specification, cancellationToken);

        var phrases = (await sender.Send(new GetPhrasesRequest(
                new PhrasesSpecification(
                    phraseUsages.Select(x => x.NativePhraseId)
                        .Union(phraseUsages.Select(x => x.TranslationPhraseId))
                        .ToArray())), cancellationToken))
            .Value.ToDictionary(x => x.Id, x => x);

        List<TopicDTO> topics = await sender.Send(new GetTopicsRequest(
            new TopicsSpecification(phraseUsages.Select(x => x.TopicId).ToArray())), cancellationToken);

        var result = phraseUsages.Select(x => new PhraseUsageDTO(x.Id,
            topics.Single(y => y.Id == x.TopicId), phrases[x.NativePhraseId], phrases[x.TranslationPhraseId], 
            x.NativeSentence, x.TranslatedSentence, x.PhotoUrl)).ToList();

        return result;
    }
}