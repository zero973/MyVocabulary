using Ardalis.Specification;
using MyVocabulary.Application.Models;
using MyVocabulary.Domain.Entities;

namespace MyVocabulary.Application.Specifications;

public class PhrasesSpecification : Specification<Phrase>
{

    public PhrasesSpecification(uint skip, uint take)
    {
        Query.Skip((int)skip).Take((int)take);
    }

    public PhrasesSpecification(uint skip, uint take, Language language)
        : this(skip, take)
    {
        Query.Where(x => x.Culture == language.Value);
    }

    public PhrasesSpecification(uint skip, uint take, string phrase)
        : this(skip, take)
    {
        Query.Where(x => x.Value.ToLower().Contains(phrase.ToLower()));
    }

    public PhrasesSpecification(uint skip, uint take, Language language, string phrase)
    {
        Query.Where(x => x.Culture == language.Value
            && x.Value.ToLower().Contains(phrase.ToLower()));
    }

    public PhrasesSpecification(params Guid[] phraseIds)
    {
        Query.Where(x => phraseIds.Contains(x.Id));
    }

}