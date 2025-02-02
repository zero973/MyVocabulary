using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;

namespace MyVocabulary.Application.Commands.Topics;

/// <summary>
/// Add topic
/// </summary>
public record AddTopicRequest(TopicDTO Entity) : IRequest<Result<TopicDTO>>;

/// <summary>
/// Edit topic
/// </summary>
public record EditTopicRequest(TopicDTO Entity) : IRequest<Result<TopicDTO>>;

/// <summary>
/// Delete topic
/// </summary>
public record DeleteTopicRequest(Guid Id) : IRequest<Result>;