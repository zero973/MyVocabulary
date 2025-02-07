using Ardalis.Specification;
using MyVocabulary.Application.Models;
using MyVocabulary.Domain.Entities;

namespace MyVocabulary.Application.Specifications;

public class PhraseSpecification : Specification<Phrase>
{

    public PhraseSpecification(Guid id)
    {
        Query.Where(x => x.Id == id);
    }

    public PhraseSpecification(string value, Language language)
    {
        Query.Where(x => x.Value == value && x.Culture == language.Value);
    }

}