using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.Phrases;
using MyVocabulary.Application.Specifications;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Queries.Topics.Handlers;

internal class GetTopicHandler : IRequestHandler<GetTopicRequest, Result<TopicDTO>>
{

    private readonly IRepository<Topic> _topicsRepository;
    private readonly ISender _sender;

    public GetTopicHandler(IRepository<Topic> topicsRepository, ISender sender)
    {
        _topicsRepository = topicsRepository;
        _sender = sender;
    }

    public async Task<Result<TopicDTO>> Handle(GetTopicRequest request, CancellationToken cancellationToken)
    {
        var topic = (await _topicsRepository.FirstOrDefaultAsync(
            new TopicsSpecification(request.Id, true)))!;

        var phraseUsages = new List<PhraseUsageDTO>();
        var phraseIds = topic.PhraseUsages.Select(x => x.NativePhraseId)
            .Union(topic.PhraseUsages.Select(x => x.TranslationPhraseId))
            .ToArray();

        var phrases = (await _sender.Send(new GetPhrasesRequest(new PhrasesSpecification(phraseIds))))
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