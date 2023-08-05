using Microsoft.Data.SqlClient;

namespace Norimsoft.StringEditor.Integration.Tests;

public static class Helpers
{
    public const string DbConnectionStringData = "Server=localhost;Database=StringEditor_Tests;User Id=sa;Password=Mn9-hwL8J;Encrypt=no";
    public const string DbConnectionStringMaster = "Server=localhost;Database=master;User Id=sa;Password=Mn9-hwL8J;Encrypt=no";

    public static async Task InitDb()
    {
        await using var sqlConnection = new SqlConnection(DbConnectionStringMaster);
        await sqlConnection.OpenAsync();

        var cmd = new SqlCommand("create database StringEditor_Tests")
        {
            Connection = sqlConnection,
        };

        await cmd.ExecuteNonQueryAsync();
    }
}
