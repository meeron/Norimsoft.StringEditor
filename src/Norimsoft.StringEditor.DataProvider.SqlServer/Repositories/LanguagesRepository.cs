using Microsoft.Data.SqlClient;
using Norimsoft.StringEditor.DataProvider.Models;
using RepoDb;
using RepoDb.Enumerations;

namespace Norimsoft.StringEditor.DataProvider.SqlServer.Repositories;

internal class LanguagesRepository : IRepository<Language>
{
    private readonly SqlConnection _connection;
    private readonly SqlServerDataProviderOptions _options;

    public LanguagesRepository(SqlConnection connection, SqlServerDataProviderOptions options)
    {
        _connection = connection;
        _options = options;
    }

    public async Task<IReadOnlyCollection<Language>> Get(CancellationToken ct)
    {
        var items = await _connection.QueryAllAsync<Language>(
            cancellationToken: ct,
            orderBy: new List<OrderField> { new ("Code", Order.Ascending) });
        return items.ToArray();
    }

    public async Task<Language?> Get(int id, CancellationToken ct)
    {
        var items = await _connection.QueryAsync<Language>(x => x.Id == id, cancellationToken: ct);

        return items.SingleOrDefault();
    }

    public async Task<Language> Insert(Language newEntity, CancellationToken ct)
    {
        var id = await _connection.InsertAsync(newEntity, cancellationToken: ct);
        newEntity.Id = (int)id;

        return newEntity;
    }

    public Task<int> Delete(int id, CancellationToken ct) =>
        _connection.DeleteAsync<Language>(x => x.Id == id, cancellationToken: ct);

    public Task<int> Update(Language entity, CancellationToken ct) =>
        _connection.UpdateAsync(entity, x => x.Id == entity.Id, cancellationToken: ct);
}
