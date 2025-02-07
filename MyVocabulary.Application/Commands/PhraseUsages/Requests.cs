using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;

namespace MyVocabulary.Application.Commands.PhraseUsages;

/// <summary>
/// Add phrase usage
/// </summary>
public record AddPhraseUsageRequest(PhraseUsageDTO Entity) : IRequest<Result<PhraseUsageDTO>>;

/// <summary>
/// Edit phrase usage
/// </summary>
public record EditPhraseUsageRequest(PhraseUsageDTO Entity) : IRequest<Result<PhraseUsageDTO>>;

/// <summary>
/// Delete phrase usage
/// </summary>
public record DeletePhraseUsageRequest(Guid Id) : IRequest<Result>;