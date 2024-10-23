using Ardalis.SharedKernel;
using Ardalis.Specification.EntityFrameworkCore;

namespace infrastrucrue.Database;

public class EfRepository<T>(ThanachartDbContext dbContext)
    : RepositoryBase<T>(dbContext), IReadRepository<T>, IRepository<T>
    where T : class, IAggregateRoot;
