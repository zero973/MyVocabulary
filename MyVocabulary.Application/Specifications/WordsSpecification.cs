using System.Globalization;
using Ardalis.Specification;
using MyVocabulary.Domain.Entities;

namespace MyVocabulary.Application.Specifications;

public class WordsSpecification : Specification<Word>
{

    public WordsSpecification(uint skip, uint take)
    {
        Query.Skip((int)skip).Take((int)take);
    }

    public WordsSpecification(uint skip, uint take, CultureInfo culture)
        : this(skip, take)
    {
        Query.Where(x => x.Culture == culture.EnglishName);
    }

    public WordsSpecification(uint skip, uint take, string word)
        : this(skip, take)
    {
        Query.Where(x => x.Value.ToLower().Contains(word.ToLower()));
    }

    public WordsSpecification(Guid[] wordIds)
    {
        Query.Where(x => wordIds.Contains(x.Id));
    }

}