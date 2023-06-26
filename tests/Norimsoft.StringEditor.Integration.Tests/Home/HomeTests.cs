using System.Net;
using Shouldly;
using Xunit;

namespace Norimsoft.StringEditor.Integration.Tests.Home;

public class HomeTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;

    public HomeTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Get_StringsHtml_WithIndex_ShouldReturn_Ok()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.GetAsync("/strings");
        var contentType = response.Content.Headers.ContentType?.ToString();

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        contentType.ShouldBe("text/html");
    }
}
