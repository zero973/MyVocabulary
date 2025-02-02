using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Specifications;

namespace MyVocabulary.Application.Queries.Topics;

/// <summary>
/// Get topic by Id
/// </summary>
public sealed record GetTopicRequest(Guid Id) : IRequest<Result<TopicDTO>>;

/// <summary>
/// Get topics with filter
/// </summary>
public sealed record GetTopicsRequest(TopicsSpecification Specification) : IRequest<Result<List<TopicDTO>>>;