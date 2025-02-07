using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Specifications;

namespace MyVocabulary.Application.Queries.PhraseUsages;

/// <summary>
/// Get phrase usage by Id
/// </summary>
public sealed record GetPhraseUsageRequest(Guid Id) : IRequest<Result<PhraseUsageDTO>>;

/// <summary>
/// Get phrase usages with filter
/// </summary>
public sealed record GetPhraseUsagesRequest(PhraseUsagesSpecification Specification)
    : IRequest<Result<List<PhraseUsageDTO>>>;