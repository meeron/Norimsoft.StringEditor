using Microsoft.AspNetCore.Http.HttpResults;
using Norimsoft.StringEditor.DataProvider.Models;
using Norimsoft.StringEditor.Endpoints.Apps;
using NSubstitute;
using Shouldly;

namespace Norimsoft.StringEditor.Tests.Apps;

public class GetAppsEndpointTests : AppEndpointTests
{
    [Fact]
    public async Task ReturnOkResult()
    {
        // Arrange
        MockAppRepository.Get(CancellationToken.None).Returns(new App[] { new() });
        
        // Act
        var result = await GetAppsEndpoint.Handler(MockDbContext);
        var ok = result as Ok<IReadOnlyCollection<App>>;

        // Assert
        ok.ShouldNotBeNull();
        ok.Value?.Count.ShouldBe(1);
    }
}