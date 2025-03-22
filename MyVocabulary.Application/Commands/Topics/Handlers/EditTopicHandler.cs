using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.Topics;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Commands.Topics.Handlers;

internal class EditTopicHandler(IRepository<Topic> topicsRepository, ISender sender)
    : IRequestHandler<EditTopicRequest, Result<TopicDTO>>
{
    public async Task<Result<TopicDTO>> Handle(EditTopicRequest request, CancellationToken cancellationToken)
    {
        var topic = await topicsRepository.GetByIdAsync(request.Entity.Id, cancellationToken);
        topic!.Edit(request.Entity.CultureFrom.Value,
            request.Entity.CultureTo.Value,
            request.Entity.Header, 
            request.Entity.Description, 
            request.Entity.PhotoUrl);

        await topicsRepository.UpdateAsync(topic, cancellationToken);
        var topicDto = await sender.Send(new GetTopicRequest(topic.Id), cancellationToken);

        return topicDto;
    }
}