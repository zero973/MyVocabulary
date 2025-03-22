using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.Phrases;
using MyVocabulary.Application.Specifications;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Queries.Topics.Handlers;

internal class GetTopicHandler(IRepository<Topic> topicsRepository, ISender sender)
    : IRequestHandler<GetTopicRequest, Result<TopicDTO>>
{
    public async Task<Result<TopicDTO>> Handle(GetTopicRequest request, CancellationToken cancellationToken)
    {
        var topic = (await topicsRepository.FirstOrDefaultAsync(
            new TopicsSpecification(request.Id, true), cancellationToken))!;

        var phraseUsages = new List<PhraseUsageDTO>();
        var phraseIds = topic.PhraseUsages.Select(x => x.NativePhraseId)
            .Union(topic.PhraseUsages.Select(x => x.TranslationPhraseId))
            .ToArray();

        var phrases = (await sender.Send(new GetPhrasesRequest(
                new PhrasesSpecification(phraseIds)), cancellationToken))
            .Value.ToDictionary(x => x.Id, x => x);

        var result = new TopicDTO(topic.Id,
            new Language(topic.CultureFrom),
            new Language(topic.CultureTo),
            topic.Header,
            topic.Description,
            topic.PhotoUrl,
            phraseUsages);

        foreach (var w in topic.PhraseUsages)
            phraseUsages.Add(new PhraseUsageDTO(w.Id,
                result, 
                phrases[w.NativePhraseId], 
                phrases[w.TranslationPhraseId], 
                w.NativeSentence, 
                w.TranslatedSentence, 
                w.PhotoUrl));

        return result;
    }
}