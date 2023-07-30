using System.Data.SqlClient;
using Norimsoft.StringEditor.DataProvider.Models;
using Norimsoft.StringEditor.Endpoints.Apps.Models;

namespace Norimsoft.StringEditor.Integration.Tests.Apps;

public class AppsTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;

    public AppsTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetAppsEndpoint_Should_ReturnOk()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.GetAsync("strings/api/v1/apps");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        await response.Content.ReadFromJsonAsync<App[]>();
    }

    [Fact]
    public async Task CreateAppEndpoint_EmptyBody_Should_ReturnBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        var body = new CreateAppBody();
        
        // Act
        var response = await client.PostAsJsonAsync("strings/api/v1/apps", body);
        var data = await response.Content.ReadFromJsonAsync<ErrorResult>();
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        data!.Message.ShouldBe("'Display Text' must not be empty.");
    }
    
    [Fact]
    public async Task CreateAppEndpoint_BodyHasDisplayText_Should_ReturnCreatedWithApp()
    {
        // Arrange
        const string displayText = "Test app";
        const string slug = "test-app";
        
        var client = _factory.CreateClient();
        var body = new CreateAppBody
        {
            DisplayText = displayText,
        };
        
        // Act
        var response = await client.PostAsJsonAsync("strings/api/v1/apps", body);
        var data = await response.Content.ReadFromJsonAsync<App>();
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        data!.DisplayText.ShouldBe(displayText);
        data.Slug.ShouldBe(slug);
    }
}
