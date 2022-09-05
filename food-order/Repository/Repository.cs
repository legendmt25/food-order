using Microsoft.EntityFrameworkCore;

namespace Repository;
public class Repository<TEntity> where TEntity : class
{
    protected readonly ApplicationContext context;
    protected readonly DbSet<TEntity> entries;
    public Repository(ApplicationContext context)
    {
        this.context = context;
        entries = context.Set<TEntity>();
    }
}