using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Specifications;

namespace MyVocabulary.Application.Queries.Words;

/// <summary>
/// Get words with filter
/// </summary>
public sealed record GetWordRequest(WordSpecification Specification) : IRequest<Result<WordDTO>>;

/// <summary>
/// Get words with filter
/// </summary>
public sealed record GetWordsRequest(WordsSpecification Specification) : IRequest<Result<List<WordDTO>>>;