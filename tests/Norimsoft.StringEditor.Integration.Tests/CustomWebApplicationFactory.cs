using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Norimsoft.StringEditor.Integration.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<TestProgram>
{
    public override async ValueTask DisposeAsync()
    {
        await using var sqlConnection = new SqlConnection(Helpers.DbConnectionStringMaster);
        await sqlConnection.OpenAsync();

        var cmd = new SqlCommand("drop database StringEditor_Tests")
        {
            Connection = sqlConnection,
        };

        await cmd.ExecuteNonQueryAsync();
        
        await base.DisposeAsync();
    }
}
