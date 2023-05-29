using Norimsoft.StringEditor.DataProvider.Models;

namespace Norimsoft.StringEditor.DataProvider;

public interface IDataContext : IDisposable
{
    IRepository<App> Apps { get; }
    
    IRepository<Language> Languages { get; }
}
