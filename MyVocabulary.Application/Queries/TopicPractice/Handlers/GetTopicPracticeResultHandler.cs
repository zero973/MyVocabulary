using Ardalis.Result;
using MediatR;
using MyVocabulary.Application.Commands.App;
using MyVocabulary.Application.Models;
using MyVocabulary.Application.Specifications;
using MyVocabulary.Domain.Entities;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Application.Queries.TopicPractice.Handlers;

public class GetTopicPracticeResultHandler(IRepository<UserAnswer> userAnswersRepository, ISender sender)
    : IRequestHandler<GetTopicPracticeResultRequest, Result<TopicPracticeResult>>
{
    public async Task<Result<TopicPracticeResult>> Handle(GetTopicPracticeResultRequest request, 
        CancellationToken cancellationToken)
    {
        var settings = (await sender.Send(new LoadUserSettingsRequest())).Value;
        var answers = await userAnswersRepository.ListAsync(
            new UserAnswersSpecification(request.Topic.PhraseUsages.Select(x => x.Id).ToArray(), 
                settings.CountMonthsValidAnswers));

        var correctPhraseUsages = answers.Where(x => x.IsRight)
            .DistinctBy(x => x.PhraseUsageId).ToList();
        var wrongPhraseUsagesCount = answers
            .Where(x => correctPhraseUsages.All(y => y.PhraseUsageId != x.PhraseUsageId) && !x.IsRight)
            .DistinctBy(x => x.PhraseUsageId).Count();
        
        return new TopicPracticeResult((uint)correctPhraseUsages.Count, (uint)wrongPhraseUsagesCount, 
            (double)correctPhraseUsages.Count / request.Topic.PhraseUsages.Count);
    }
}