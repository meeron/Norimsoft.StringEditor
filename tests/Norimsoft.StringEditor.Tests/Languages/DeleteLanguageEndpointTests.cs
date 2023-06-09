using Microsoft.AspNetCore.Http.HttpResults;
using Norimsoft.StringEditor.Endpoints.Languages;
using NSubstitute;
using Shouldly;

namespace Norimsoft.StringEditor.Tests.Languages;

public class DeleteLanguageEndpointTests : LanguagesEndpointTests
{
    [Fact]
    public async Task ReturnOkResult()
    {
        // Arrange
        const int id = 666;

        MockRepository.Delete(id, CancellationToken.None).Returns(1);
        
        // Act
        var result = await DeleteLanguageEndpoint.Handler(id, MockDbContext);
        var ok = result as Ok<DeletedResult>;
        
        // Assert
        ok.ShouldNotBeNull();
        ok.Value?.Count.ShouldBe(1);

        await MockRepository.Received(1).Delete(id, CancellationToken.None);
    }
}