using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Queries.Topics.Handlers;

public class GetTopicsHandler : IRequestHandler<GetTopicsRequest, Result<List<TopicDTO>>>
{

    private readonly IRepository<Topic> _topicsRepository;

    public GetTopicsHandler(IRepository<Topic> topicsRepository)
    {
        _topicsRepository = topicsRepository;
    }

    public async Task<Result<List<TopicDTO>>> Handle(GetTopicsRequest request, CancellationToken cancellationToken)
    {
        var topics = await _topicsRepository.ListAsync(request.Specification);

        var result = topics.Select(x => new TopicDTO(x.Id, 
            new Language(x.CultureFrom), new Language(x.CultureTo), 
            x.Header, x.Description, x.PhotoUrl, new List<WordUsageDTO>())).ToList();

        return result;
    }

}