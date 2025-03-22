using Ardalis.Specification;
using MyVocabulary.Domain.Entities;

namespace MyVocabulary.Application.Specifications;

/// <summary>
/// Common specification
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class GenericSpecification<T> : Specification<T>
    where T : BaseEntity
{
    public GenericSpecification(Guid id)
    {
        Query.Where(x => x.Id == id);
    }
}