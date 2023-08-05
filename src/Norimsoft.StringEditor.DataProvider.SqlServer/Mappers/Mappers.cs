using Norimsoft.StringEditor.DataProvider.Models;
using RepoDb;

namespace Norimsoft.StringEditor.DataProvider.SqlServer.Mappers;

internal static class Mappers
{
    internal static void Init(SqlServerDataProviderOptions options)
    {
        ClassMapper.Add<Language>($"{options.Schema}.Languages");
    }
}
