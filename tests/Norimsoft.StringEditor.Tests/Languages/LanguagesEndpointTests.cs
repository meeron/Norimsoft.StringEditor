using Norimsoft.StringEditor.DataProvider;
using Norimsoft.StringEditor.DataProvider.Models;
using NSubstitute;

namespace Norimsoft.StringEditor.Tests.Languages;

public abstract class LanguagesEndpointTests
{
    protected readonly IDataContext MockDbContext;
    protected readonly IRepository<Language> MockRepository;

    protected LanguagesEndpointTests()
    {
        MockRepository = Substitute.For<IRepository<Language>>();
        MockDbContext = Substitute.For<IDataContext>();

        MockDbContext.Languages.Returns(MockRepository);
    }
}