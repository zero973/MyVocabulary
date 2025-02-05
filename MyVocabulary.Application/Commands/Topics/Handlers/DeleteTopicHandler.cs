using Ardalis.Result;
using MediatR;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Commands.Topics.Handlers;

internal class DeleteTopicHandler : IRequestHandler<DeleteTopicRequest, Result>
{

    private readonly IRepository<Topic> _topicsRepository;

    public DeleteTopicHandler(IRepository<Topic> topicsRepository)
    {
        _topicsRepository = topicsRepository;
    }

    public async Task<Result> Handle(DeleteTopicRequest request, CancellationToken cancellationToken)
    {
        var topic = await _topicsRepository.GetByIdAsync(request.Id);
        await _topicsRepository.DeleteAsync(topic!);
        return Result.Success();
    }

}