using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.Topics;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Commands.Topics.Handlers;

internal class EditTopicHandler : IRequestHandler<EditTopicRequest, Result<TopicDTO>>
{

    private readonly IRepository<Topic> _topicsRepository;
    private readonly ISender _sender;

    public EditTopicHandler(IRepository<Topic> topicsRepository, ISender sender)
    {
        _topicsRepository = topicsRepository;
        _sender = sender;
    }

    public async Task<Result<TopicDTO>> Handle(EditTopicRequest request, CancellationToken cancellationToken)
    {
        var topic = await _topicsRepository.GetByIdAsync(request.Entity.Id);
        topic!.Edit(request.Entity.CultureFrom.Value,
            request.Entity.CultureTo.Value,
            request.Entity.Header, 
            request.Entity.Description, 
            request.Entity.PhotoUrl);

        await _topicsRepository.UpdateAsync(topic);
        var topicDto = await _sender.Send(new GetTopicRequest(topic.Id));

        return topicDto;
    }

}