using Ardalis.Result;
using MediatR;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Commands.Topics.Handlers;

internal class DeleteTopicHandler(IRepository<Topic> topicsRepository) : IRequestHandler<DeleteTopicRequest, Result>
{
    public async Task<Result> Handle(DeleteTopicRequest request, CancellationToken cancellationToken)
    {
        var topic = await topicsRepository.GetByIdAsync(request.Id, cancellationToken);
        await topicsRepository.DeleteAsync(topic!, cancellationToken);
        return Result.Success();
    }
}