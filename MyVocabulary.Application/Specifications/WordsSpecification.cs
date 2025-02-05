using Ardalis.Specification;
using MyVocabulary.Application.Models;
using MyVocabulary.Domain.Entities;

namespace MyVocabulary.Application.Specifications;

public class WordsSpecification : Specification<Word>
{

    public WordsSpecification(uint skip, uint take)
    {
        Query.Skip((int)skip).Take((int)take);
    }

    public WordsSpecification(uint skip, uint take, Language language)
        : this(skip, take)
    {
        Query.Where(x => x.Culture == language.Value);
    }

    public WordsSpecification(uint skip, uint take, string word)
        : this(skip, take)
    {
        Query.Where(x => x.Value.ToLower().Contains(word.ToLower()));
    }

    public WordsSpecification(uint skip, uint take, Language language, string word)
    {
        Query.Where(x => x.Culture == language.Value
            && x.Value.ToLower().Contains(word.ToLower()));
    }

    public WordsSpecification(params Guid[] wordIds)
    {
        Query.Where(x => wordIds.Contains(x.Id));
    }

}