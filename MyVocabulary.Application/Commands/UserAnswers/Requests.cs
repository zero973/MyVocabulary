using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;

namespace MyVocabulary.Application.Commands.UserAnswers;

/// <summary>
/// Add user answer
/// </summary>
public record AddUserAnswerRequest(UserAnswerDTO Entity) : IRequest<Result<UserAnswerDTO>>;

/// <summary>
/// Add user answers
/// </summary>
public record AddUserAnswersRequest(List<UserAnswerDTO> Answers) : IRequest<Result>;