using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Specifications;
namespace MyVocabulary.Application.Queries.WordUsages;

/// <summary>
/// Get word usages with filter
/// </summary>
public sealed record GetWordUsagesRequest(WordUsagesSpecification Specification)
    : IRequest<Result<List<WordUsageDTO>>>;