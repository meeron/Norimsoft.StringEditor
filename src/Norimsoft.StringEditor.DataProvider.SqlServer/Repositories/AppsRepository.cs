using System.Data;
using System.Data.SqlClient;
using Norimsoft.StringEditor.DataProvider.Models;

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
        var result = new List<App>();

        var cmd = new SqlCommand($"select Id, Slug, DisplayText from {_options.Schema}.Apps")
        {
            Connection = _connection,
            CommandTimeout = _options.CommandTimeout,
        };
        await using var reader = await cmd.ExecuteReaderAsync(ct);

        while (await reader.ReadAsync(ct))
        {
            result.Add(new App
            {
                Id = reader.GetInt32(0),
                Slug = reader.GetString(1),
                DisplayText = reader.GetString(2),
            });
        }

        return result;
    }

    public async Task<App?> Get(int id, CancellationToken ct)
    {
        var cmd = new SqlCommand($"select Id, Slug, DisplayText from {_options.Schema}.Apps where Id = @Id")
        {
            Connection = _connection,
            CommandTimeout = _options.CommandTimeout,
        };
        var idParam = cmd.Parameters.Add("@Id", SqlDbType.Int);
        idParam.Value = id;
        
        await using var reader = await cmd.ExecuteReaderAsync(ct);
        if (!await reader.ReadAsync(ct)) return null;

        return new App
        {
            Id = reader.GetInt32(0),
            Slug = reader.GetString(1),
            DisplayText = reader.GetString(2),
        };
    }

    public async Task<App> Insert(App newApp, CancellationToken ct)
    {
        var cmd = new SqlCommand($@"
insert into {_options.Schema}.Apps (Slug, DisplayText) values (@Slug, @DisplayText)
select @@IDENTITY
")
        {
            Connection = _connection,
            CommandTimeout = _options.CommandTimeout,
        };
        var slugParam = cmd.Parameters.Add("@Slug", SqlDbType.NVarChar, 50);
        slugParam.Value = newApp.Slug;
        
        var displayTextParam = cmd.Parameters.Add("@DisplayText", SqlDbType.NVarChar, 50);
        displayTextParam.Value = newApp.DisplayText;

        var newId = Convert.ToInt32(await cmd.ExecuteScalarAsync(ct));

        return (await Get(newId, ct))!;
    }

    public async Task<int> Delete(int id, CancellationToken ct)
    {
        var cmd = new SqlCommand($"delete from {_options.Schema}.Apps where Id = @Id")
        {
            Connection = _connection,
            CommandTimeout = _options.CommandTimeout,
        };
        var idParam = cmd.Parameters.Add("@Id", SqlDbType.Int);
        idParam.Value = id;

        return await cmd.ExecuteNonQueryAsync();
    }

    public async Task<int> Update(App app, CancellationToken ct)
    {
        var cmd = new SqlCommand($@"
update {_options.Schema}.Apps set
    Slug = @Slug,
    DisplayText = @DisplayText
where Id = @Id and (Slug <> @Slug or DisplayText <> @DisplayText)")
        {
            Connection = _connection,
            CommandTimeout = _options.CommandTimeout,
        };
        var idParam = cmd.Parameters.Add("@Id", SqlDbType.Int);
        idParam.Value = app.Id;
        
        var slugParam = cmd.Parameters.Add("@Slug", SqlDbType.NVarChar, 50);
        slugParam.Value = app.Slug;
        
        var displayTextParam = cmd.Parameters.Add("@DisplayText", SqlDbType.NVarChar, 50);
        displayTextParam.Value = app.DisplayText;

        return await cmd.ExecuteNonQueryAsync();
    }
}
