using Ardalis.Specification;
using MyVocabulary.Application.Models;
using MyVocabulary.Domain.Entities;

namespace MyVocabulary.Application.Specifications;

public class TopicsSpecification : Specification<Topic>
{

    public TopicsSpecification(uint skip, uint take)
    {
        Query.Skip((int)skip).Take((int)take);
    }

    public TopicsSpecification(uint skip, uint take, string header)
        : this(skip, take)
    {
        Query.Where(x => x.Header.ToLower().Contains(header.ToLower()));
    }

    public TopicsSpecification(uint skip, uint take, Language cultureFrom)
        : this(skip, take)
    {
        Query.Where(x => x.CultureFrom == cultureFrom.Value);
    }

    public TopicsSpecification(uint skip, uint take, Language cultureFrom, Language cultureTo)
        : this(skip, take)
    {
        Query.Where(x => x.CultureFrom == cultureFrom.Value
            && x.CultureTo == cultureTo.Value);
    }

    public TopicsSpecification(uint skip, uint take, string header, 
        Language cultureFrom, Language cultureTo)
        : this(skip, take, cultureFrom, cultureTo)
    {
        Query.Where(x => x.Header.ToLower().Contains(header.ToLower()));
    }

    public TopicsSpecification(Guid[] topicIds)
    {
        Query.Where(x => topicIds.Contains(x.Id));
    }

    /// <summary>
    /// Search topic by id and load word usages if requires.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="isLoadWordUsages"></param>
    public TopicsSpecification(Guid id, bool isLoadWordUsages)
    {
        if (isLoadWordUsages)
            Query
                .Include(x => x.WordUsages)
                .Where(x => x.Id == id);
        else
            Query.Where(x => x.Id == id);
    }

}