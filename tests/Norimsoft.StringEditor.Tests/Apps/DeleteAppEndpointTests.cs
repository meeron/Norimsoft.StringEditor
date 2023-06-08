using Microsoft.AspNetCore.Http.HttpResults;
using Norimsoft.StringEditor.Endpoints.Apps;
using NSubstitute;
using Shouldly;

namespace Norimsoft.StringEditor.Tests.Apps;

public class DeleteAppEndpointTests : AppEndpointTests
{
    [Fact]
    public async Task ReturnOkResult()
    {
        // Arrange
        const int id = 666;

        MockAppRepository.Delete(id, CancellationToken.None).Returns(1);
        
        // Act
        var result = await DeleteAppEndpoint.Handler(id, MockDbContext);
        var ok = result as Ok<DeletedResult>;
        
        // Assert
        ok.ShouldNotBeNull();
        ok.Value?.Count.ShouldBe(1);
    }
}