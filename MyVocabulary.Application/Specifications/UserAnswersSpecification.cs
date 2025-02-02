using Ardalis.Specification;
using MyVocabulary.Domain.Entities;

namespace MyVocabulary.Application.Specifications;

public class UserAnswersSpecification : Specification<UserAnswer>
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

    public UserAnswersSpecification(uint skip, uint take, Guid[] wordUsageIds)
        : this(skip, take)
    {
        Query.Where(x => wordUsageIds.Contains(x.WordUsageId));
    }

    public UserAnswersSpecification(uint skip, uint take, bool isCorrect, Guid[] wordUsageIds)
        : this(skip, take)
    {
        Query.Where(x => x.IsRight == isCorrect && wordUsageIds.Contains(x.WordUsageId));
    }

}