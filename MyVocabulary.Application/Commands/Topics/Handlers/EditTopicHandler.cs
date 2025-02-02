using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.Topics;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Commands.Topics.Handlers;

public class EditTopicHandler : IRequestHandler<EditTopicRequest, Result<TopicDTO>>
{

    private readonly IRepository<Topic> _topicRepository;
    private readonly ISender _sender;

    public EditTopicHandler(IRepository<Topic> topicRepository, ISender sender)
    {
        _topicRepository = topicRepository;
        _sender = sender;
    }

    public async Task<Result<TopicDTO>> Handle(EditTopicRequest request, CancellationToken cancellationToken)
    {
        var topic = await _topicRepository.GetByIdAsync(request.Entity.Id);
        topic!.Edit(request.Entity.CultureFrom.Value,
            request.Entity.CultureTo.Value,
            request.Entity.Header, 
            request.Entity.Description, 
            request.Entity.PhotoUrl);

        await _topicRepository.UpdateAsync(topic);
        var topicDto = await _sender.Send(new GetTopicRequest(topic.Id));

        return topicDto;
    }

}