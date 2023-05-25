using System.Data;
using System.Data.SqlClient;
using Norimsoft.StringEditor.DataProvider.Models;

namespace Norimsoft.StringEditor.DataProvider.SqlServer;

public class SqlServerDataProvider : IStringEditorDataProvider
{
    private readonly SqlServerDataProviderOptions _options;
    private SqlConnection? _connection;

    public SqlServerDataProvider(IDataProviderOptions options)
    {
        _options = (SqlServerDataProviderOptions)options;
    }

    public async Task<IReadOnlyCollection<App>> GetApps(CancellationToken ct)
    {
        var result = new List<App>();

        var cmd = new SqlCommand($"select Id, Slug, DisplayText from {_options.Schema}.Apps")
        {
            Connection = await Connection(),
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

    public async Task<App?> GetApp(int id, CancellationToken ct)
    {
        var cmd = new SqlCommand($"select Id, Slug, DisplayText from {_options.Schema}.Apps where Id = @Id")
        {
            Connection = await Connection(),
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

    public async Task<App> InsertApp(App newApp, CancellationToken ct)
    {
        var cmd = new SqlCommand($@"
insert into {_options.Schema}.Apps (Slug, DisplayText) values (@Slug, @DisplayText)
select @@IDENTITY
")
        {
            Connection = await Connection(),
            CommandTimeout = _options.CommandTimeout,
        };
        var slugParam = cmd.Parameters.Add("@Slug", SqlDbType.NVarChar, 50);
        slugParam.Value = newApp.Slug;
        
        var displayTextParam = cmd.Parameters.Add("@DisplayText", SqlDbType.NVarChar, 50);
        displayTextParam.Value = newApp.DisplayText;

        var newId = Convert.ToInt32(await cmd.ExecuteScalarAsync(ct));

        return (await GetApp(newId, ct))!;
    }

    public async Task<int> DeleteApp(int id, CancellationToken ct)
    {
        var cmd = new SqlCommand($"delete from {_options.Schema}.Apps where Id = @Id")
        {
            Connection = await Connection(),
            CommandTimeout = _options.CommandTimeout,
        };
        var idParam = cmd.Parameters.Add("@Id", SqlDbType.Int);
        idParam.Value = id;

        return await cmd.ExecuteNonQueryAsync();
    }

    public async Task<int> UpdateApp(App app, CancellationToken ct)
    {
        var cmd = new SqlCommand($@"
update {_options.Schema}.Apps set
    Slug = @Slug,
    DisplayText = @DisplayText
where Id = @Id and (Slug <> @Slug or DisplayText <> @DisplayText)")
        {
            Connection = await Connection(),
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

    public void Dispose()
    {
        _connection?.Close();
        _connection?.Dispose();
    }

    private async Task<SqlConnection> Connection()
    {
        if (_connection != null) return _connection;
        
        _connection = new SqlConnection(_options.ConnectionString);
        await _connection.OpenAsync();

        return _connection;
    }
}
