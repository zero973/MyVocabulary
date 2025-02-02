using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Specifications;

namespace MyVocabulary.Application.Queries.UserAnswers;

/// <summary>
/// Get user answers with filter
/// </summary>
public sealed record GetUserAnswersRequest(UserAnswersSpecification Specification) 
    : IRequest<Result<List<UserAnswerDTO>>>;