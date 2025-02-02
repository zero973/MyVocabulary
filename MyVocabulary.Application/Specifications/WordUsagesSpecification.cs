using Ardalis.Specification;
using MyVocabulary.Domain.Entities;

namespace MyVocabulary.Application.Specifications;

public class WordUsagesSpecification : Specification<WordUsage>
{

    public WordUsagesSpecification(uint skip, uint take)
    {
        Query.Skip((int)skip).Take((int)take);
    }

    public WordUsagesSpecification(uint skip, uint take, Guid topicId)
        : this(skip, take)
    {
        Query.Where(x => x.TopicId == topicId);
    }

    public WordUsagesSpecification(Guid[] wordUsageIds)
    {
        Query.Where(x => wordUsageIds.Contains(x.Id));
    }

}