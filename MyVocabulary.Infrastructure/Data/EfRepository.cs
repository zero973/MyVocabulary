using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Infrastructure.Data;

internal class EfRepository<T>(AppDbContext dbContext) : RepositoryBase<T>(dbContext), IReadRepository<T>, IRepository<T>
    where T : class, IAggregateRoot
{
    public override async Task<int> DeleteRangeAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification).ExecuteDeleteAsync(cancellationToken);
    }
}