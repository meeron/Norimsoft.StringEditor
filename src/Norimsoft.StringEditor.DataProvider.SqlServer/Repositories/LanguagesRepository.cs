using System.Data.SqlClient;
using Norimsoft.StringEditor.DataProvider.Models;

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

    public Task<IReadOnlyCollection<Language>> Get(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Language?> Get(int id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Language> Insert(Language newEntity, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<int> Delete(int id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<int> Update(Language entity, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
