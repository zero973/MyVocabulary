using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Queries.Topics.Handlers;

internal class GetTopicsHandler(IRepository<Topic> topicsRepository)
    : IRequestHandler<GetTopicsRequest, Result<List<TopicDTO>>>
{
    public async Task<Result<List<TopicDTO>>> Handle(GetTopicsRequest request, CancellationToken cancellationToken)
    {
        var topics = await topicsRepository.ListAsync(request.Specification, cancellationToken);

        var result = topics.Select(x => new TopicDTO(x.Id, 
            new Language(x.CultureFrom), new Language(x.CultureTo), 
            x.Header, x.Description, x.PhotoUrl, [])).ToList();

        return result;
    }

}