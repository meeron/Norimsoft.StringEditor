namespace Norimsoft.StringEditor.DataProvider;

public interface IMigrationProvider : IDisposable
{
    Task Migrate();
}