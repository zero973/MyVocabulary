using Ardalis.Specification.EntityFrameworkCore;
using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Infrastructure.Data;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> 
    where T : class, IAggregateRoot
{
    public EfRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}