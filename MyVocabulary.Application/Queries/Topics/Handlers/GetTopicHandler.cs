using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.Words;
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

        var wordUsages = new List<WordUsageDTO>();
        var wordIds = topic.WordUsages.Select(x => x.NativeWordId)
            .Union(topic.WordUsages.Select(x => x.TranslationWordId))
            .ToArray();

        var words = (await _sender.Send(new GetWordsRequest(new WordsSpecification(wordIds))))
            .Value.ToDictionary(x => x.Id, x => x);

        var result = new TopicDTO(topic.Id,
            new Language(topic.CultureFrom),
            new Language(topic.CultureTo),
            topic.Header,
            topic.Description,
            topic.PhotoUrl,
            wordUsages);

        foreach (var w in topic.WordUsages)
            wordUsages.Add(new WordUsageDTO(w.Id,
                result, 
                words[w.NativeWordId], 
                words[w.TranslationWordId], 
                w.NativeSentence, 
                w.TranslatedSentence, 
                w.PhotoUrl));

        return result;
    }
}