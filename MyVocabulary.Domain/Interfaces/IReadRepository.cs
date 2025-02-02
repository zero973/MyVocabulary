using Ardalis.Specification;

namespace MyVocabulary.Domain.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{ }