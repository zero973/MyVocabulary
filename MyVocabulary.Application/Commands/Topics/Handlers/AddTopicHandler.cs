using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.Topics;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Commands.Topics.Handlers;

internal class AddTopicHandler : IRequestHandler<AddTopicRequest, Result<TopicDTO>>
{

    private readonly IRepository<Topic> _topicsRepository;
    private readonly ISender _sender;

    public AddTopicHandler(IRepository<Topic> topicsRepository, ISender sender)
    {
        _topicsRepository = topicsRepository;
        _sender = sender;
    }

    public async Task<Result<TopicDTO>> Handle(AddTopicRequest request, CancellationToken cancellationToken)
    {
        var wordUsages = request.Entity.WordUsages.Select(x => new WordUsage(x.Topic.Id, 
            x.NativeWord.Id, 
            x.TranslationWord.Id, 
            x.NativeSentence, 
            x.TranslatedSentence, 
            x.PhotoUrl)).ToList();

        var topic = new Topic(request.Entity.CultureFrom.Value,
            request.Entity.CultureTo.Value,
            request.Entity.Header,
            request.Entity.Description,
            request.Entity.PhotoUrl,
            wordUsages);

        var result = await _topicsRepository.AddAsync(topic);
        var topicDto = await _sender.Send(new GetTopicRequest(result.Id));

        // todo check need I add wordUsages manually like this
        // _wordUsageRepository.AddRange(wordUsages);

        return topicDto;
    }

}