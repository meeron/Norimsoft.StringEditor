using Norimsoft.StringEditor.DataProvider.Models;

namespace Norimsoft.StringEditor.DataProvider;

public interface IAppsRepository
{
    Task<IReadOnlyCollection<App>> GetApps(CancellationToken ct);
    
    Task<App?> GetApp(int id, CancellationToken ct);

    Task<App> InsertApp(App newApp, CancellationToken ct);

    Task<int> DeleteApp(int id, CancellationToken ct);

    Task<int> UpdateApp(App app, CancellationToken ct);
}
