using Ardalis.Specification;
using MyVocabulary.Application.Models;
using MyVocabulary.Domain.Entities;

namespace MyVocabulary.Application.Specifications;

public class WordSpecification : Specification<Word>
{

    public WordSpecification(Guid id)
    {
        Query.Where(x => x.Id == id);
    }

    public WordSpecification(string value, Language language)
    {
        Query.Where(x => x.Value == value && x.Culture == language.Value);
    }

}