using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Specifications;
using MyVocabulary.Domain.Entities;

namespace MyVocabulary.Application.Queries.Words;

/// <summary>
/// Get words with filter
/// </summary>
public sealed record GetWordsRequest(WordsSpecification Specification) : IRequest<Result<List<Word>>>;