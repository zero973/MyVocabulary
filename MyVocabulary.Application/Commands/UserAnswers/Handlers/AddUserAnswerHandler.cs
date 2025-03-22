using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Specifications;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Commands.UserAnswers.Handlers;

public class AddUserAnswerHandler(IRepository<UserAnswer> userAnswersRepository)
    : IRequestHandler<AddUserAnswerRequest, Result<UserAnswerDTO>>
{
    public async Task<Result<UserAnswerDTO>> Handle(AddUserAnswerRequest request, CancellationToken cancellationToken)
    {
        // delete previous answer on this phrase usages
        await userAnswersRepository.DeleteRangeAsync(new UserAnswersSpecification([request.Entity.PhraseUsage.Id]));

        await userAnswersRepository.AddAsync(new UserAnswer(request.Entity.PhraseUsage.Id, request.Entity.IsRight));
        
        return Result.Success();
    }
}