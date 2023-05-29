namespace Norimsoft.StringEditor.DataProvider;

public interface IDataContext : IDisposable
{
    IAppsRepository Apps { get; }
}
