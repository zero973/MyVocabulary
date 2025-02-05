using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;

namespace MyVocabulary.Application.Commands.WordUsages;

/// <summary>
/// Add word usage
/// </summary>
public record AddWordUsageRequest(WordUsageDTO Entity) : IRequest<Result<WordUsageDTO>>;

/// <summary>
/// Edit word usage
/// </summary>
public record EditWordUsageRequest(WordUsageDTO Entity) : IRequest<Result<WordUsageDTO>>;

/// <summary>
/// Delete word usage
/// </summary>
public record DeleteWordUsageRequest(Guid Id) : IRequest<Result>;