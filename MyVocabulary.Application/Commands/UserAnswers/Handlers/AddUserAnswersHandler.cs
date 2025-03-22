using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Specifications;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Commands.UserAnswers.Handlers;

public class AddUserAnswersHandler(IRepository<UserAnswer> userAnswersRepository)
    : IRequestHandler<AddUserAnswersRequest, Result>
{
    public async Task<Result> Handle(AddUserAnswersRequest request, CancellationToken cancellationToken)
    {
        // delete previous answers on this phrase usages
        var phraseUsagesId = request.Answers.Select(x => x.PhraseUsage.Id).ToArray();
        await userAnswersRepository.DeleteRangeAsync(new UserAnswersSpecification(phraseUsagesId));

        var answers = request.Answers.Select(x => new UserAnswer(x.PhraseUsage.Id, x.IsRight));
        await userAnswersRepository.AddRangeAsync(answers);
        
        return Result.Success();
    }
}