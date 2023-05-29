using Norimsoft.StringEditor.DataProvider.Models;

namespace Norimsoft.StringEditor.DataProvider;

public interface IRepository<TEntity>
    where TEntity: Entity
{
    Task<IReadOnlyCollection<TEntity>> Get(CancellationToken ct);
    
    Task<TEntity?> Get(int id, CancellationToken ct);

    Task<TEntity> Insert(TEntity newEntity, CancellationToken ct);

    Task<int> Delete(int id, CancellationToken ct);

    Task<int> Update(TEntity entity, CancellationToken ct);
}
