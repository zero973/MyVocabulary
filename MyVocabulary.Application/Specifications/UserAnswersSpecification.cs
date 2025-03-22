using Ardalis.Specification;
using MyVocabulary.Domain.Entities;

namespace MyVocabulary.Application.Specifications;

public sealed class UserAnswersSpecification : Specification<UserAnswer>
{

    public UserAnswersSpecification(uint skip, uint take)
    {
        Query.Skip((int)skip).Take((int)take);
    }

    public UserAnswersSpecification(uint skip, uint take, bool isCorrect)
        : this(skip, take)
    {
        Query.Where(x => x.IsRight == isCorrect);
    }

    public UserAnswersSpecification(uint skip, uint take, Guid[] phraseUsageIds)
        : this(skip, take)
    {
        Query.Where(x => phraseUsageIds.Contains(x.PhraseUsageId));
    }

    public UserAnswersSpecification(uint skip, uint take, bool isCorrect, Guid[] phraseUsageIds)
        : this(skip, take)
    {
        Query.Where(x => x.IsRight == isCorrect && phraseUsageIds.Contains(x.PhraseUsageId));
    }

    public UserAnswersSpecification(Guid[] phraseUsageIds)
    {
        Query.Where(x => phraseUsageIds.Contains(x.PhraseUsageId));
    }

    public UserAnswersSpecification(Guid[] phraseUsageIds, uint countMonthsValidAnswers)
    {
        var thresholdDate = DateTime.UtcNow.AddMonths((int)-countMonthsValidAnswers);
        Query.Where(x => phraseUsageIds.Contains(x.PhraseUsageId) && x.Date >= thresholdDate);
    }

}