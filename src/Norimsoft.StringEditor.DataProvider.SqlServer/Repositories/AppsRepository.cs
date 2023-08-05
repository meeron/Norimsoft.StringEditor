using Microsoft.Data.SqlClient;
using Norimsoft.StringEditor.DataProvider.Models;
using RepoDb;

namespace Norimsoft.StringEditor.DataProvider.SqlServer.Repositories;

internal class AppsRepository : IRepository<App>
{
    private readonly SqlConnection _connection;
    private readonly SqlServerDataProviderOptions _options;

    internal AppsRepository(SqlConnection connection, SqlServerDataProviderOptions options)
    {
        _connection = connection;
        _options = options;
    }

    public async Task<IReadOnlyCollection<App>> Get(CancellationToken ct)
    {
        var orderBy = new[]
        {
            OrderField.Ascending<App>(x => x.DisplayText),
        };
        
        var items = await _connection.QueryAllAsync<App>(
            cancellationToken: ct,
            orderBy: orderBy);
        
        return items.ToArray();
    }

    public async Task<App?> Get(int id, CancellationToken ct)
    {
        var items = await _connection.QueryAsync<App>(
            x => x.Id == id,
            cancellationToken: ct);

        return items.SingleOrDefault();
    }

    public async Task<App> Insert(App newApp, CancellationToken ct)
    {
        var id = await _connection.InsertAsync(newApp, cancellationToken: ct);

        newApp.Id = (int)id;
        
        return newApp;
    }

    public Task<int> Delete(int id, CancellationToken ct) =>
        _connection.DeleteAsync<App>(x => x.Id == id, cancellationToken: ct);

    public Task<int> Update(App app, CancellationToken ct) =>
        _connection.UpdateAsync(app, x => x.Id == app.Id, cancellationToken: ct);
}
