using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Queries.PhraseUsages;
using MyVocabulary.Application.Specifications;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Queries.UserAnswers.Handlers;

internal class GetUserAnswersHandler(
    IRepository<UserAnswer> userAnswersRepository,
    ISender sender)
    : IRequestHandler<GetUserAnswersRequest, Result<List<UserAnswerDTO>>>
{
    public async Task<Result<List<UserAnswerDTO>>> Handle(GetUserAnswersRequest request, CancellationToken cancellationToken)
    {
        var userAnswers = await userAnswersRepository.ListAsync(request.Specification, cancellationToken);
        List<PhraseUsageDTO> phraseUsages = await sender.Send(new GetPhraseUsagesRequest(
            new PhraseUsagesSpecification(userAnswers
                .Select(x => x.PhraseUsageId).Distinct().ToArray())), cancellationToken);

        var result = userAnswers.Select(x => new UserAnswerDTO(x.Id, 
            phraseUsages.Single(y => y.Id == x.PhraseUsageId), x.IsRight)).ToList();

        return result;
    }
}