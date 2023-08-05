using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;

namespace Norimsoft.StringEditor.Integration.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<TestProgram>
{
    public override async ValueTask DisposeAsync()
    {
        await using var sqlConnection = new SqlConnection(Helpers.DbConnectionStringMaster);
        await sqlConnection.OpenAsync();

        var cmd = new SqlCommand(@"
ALTER DATABASE StringEditor_Tests SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
drop database StringEditor_Tests;")
        {
            Connection = sqlConnection,
        };

        await cmd.ExecuteNonQueryAsync();
        
        await base.DisposeAsync();
    }
}
