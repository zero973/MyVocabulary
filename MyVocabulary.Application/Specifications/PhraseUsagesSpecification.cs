using Ardalis.Specification;
using MyVocabulary.Domain.Entities;

namespace MyVocabulary.Application.Specifications;

public class PhraseUsagesSpecification : Specification<PhraseUsage>
{

    public PhraseUsagesSpecification(uint skip, uint take)
    {
        Query.Skip((int)skip).Take((int)take);
    }

    public PhraseUsagesSpecification(uint skip, uint take, Guid topicId)
        : this(skip, take)
    {
        Query.Where(x => x.TopicId == topicId);
    }

    public PhraseUsagesSpecification(Guid[] phraseUsageIds)
    {
        Query.Where(x => phraseUsageIds.Contains(x.Id));
    }

}