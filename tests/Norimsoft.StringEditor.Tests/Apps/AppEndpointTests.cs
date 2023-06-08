using Norimsoft.StringEditor.DataProvider;
using Norimsoft.StringEditor.DataProvider.Models;
using NSubstitute;

namespace Norimsoft.StringEditor.Tests.Apps;

public abstract class AppEndpointTests
{
    protected readonly IDataContext MockDbContext;
    protected readonly IRepository<App> MockAppRepository;

    protected AppEndpointTests()
    {
        MockAppRepository = Substitute.For<IRepository<App>>();
        MockDbContext = Substitute.For<IDataContext>();

        MockDbContext.Apps.Returns(MockAppRepository);
    }
}