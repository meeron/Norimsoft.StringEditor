using Microsoft.AspNetCore.Http.HttpResults;
using Norimsoft.StringEditor.DataProvider.Models;
using Norimsoft.StringEditor.Endpoints.Languages;
using NSubstitute;
using Shouldly;

namespace Norimsoft.StringEditor.Tests.Languages;

public class GetLanguagesEndpointTests : LanguagesEndpointTests
{
    [Fact]
    public async Task ReturnOkResult()
    {
        // Arrange
        MockRepository.Get(CancellationToken.None).Returns(new Language[] { new() });
        
        // Act
        var result = await GetLanguagesEndpoint.Handler(MockDbContext);
        var ok = result as Ok<IReadOnlyCollection<Language>>;

        // Assert
        ok.ShouldNotBeNull();
        ok.Value?.Count.ShouldBe(1);
    }
}