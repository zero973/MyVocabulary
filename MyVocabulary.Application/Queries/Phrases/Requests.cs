using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Specifications;

namespace MyVocabulary.Application.Queries.Phrases;

/// <summary>
/// Get phrases with filter
/// </summary>
public sealed record GetPhraseRequest(PhraseSpecification Specification) : IRequest<Result<PhraseDTO>>;

/// <summary>
/// Get phrases with filter
/// </summary>
public sealed record GetPhrasesRequest(PhrasesSpecification Specification) : IRequest<Result<List<PhraseDTO>>>;