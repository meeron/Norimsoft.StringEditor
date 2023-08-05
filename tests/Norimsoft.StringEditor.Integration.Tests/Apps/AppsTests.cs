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
        var location = response.Headers.Location?.ToString();
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        data!.DisplayText.ShouldBe(displayText);
        data.Slug.ShouldBe(slug);
        location.ShouldBe($"/apps/{data.Id}");
    }

    [Fact]
    public async Task UpdateAppEndpoint_AppNotFound_Should_Return_NotFound()
    {
        // Arrange
        var client = _factory.CreateClient();
        var body = new UpdateAppBody();
        
        // Act
        var response = await client.PutAsJsonAsync("strings/api/v1/apps/0", body);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.UnprocessableEntity);
        
        var data = await response.Content.ReadFromJsonAsync<ErrorCodeResult>();
        data!.Code.ShouldBe("NotFound");
    }
    
    [Fact]
    public async Task UpdateAppEndpoint_AppFound_BodyWithSlug_Should_Return_Ok()
    {
        // Arrange
        const string newSlug = "newSlug";
        
        var client = _factory.CreateClient();
        var body = new UpdateAppBody
        {
            Slug = newSlug,
        };
        
        // Act
        var createRes = await client.PostAsJsonAsync("strings/api/v1/apps", new CreateAppBody
        {
            DisplayText = "Test"
        });
        var app = await createRes.Content.ReadFromJsonAsync<App>();
        
        var response = await client.PutAsJsonAsync($"strings/api/v1/apps/{app!.Id}", body);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        
        var data = await response.Content.ReadFromJsonAsync<App>();
        data!.Slug.ShouldBe(newSlug);
    }
    
    [Fact]
    public async Task UpdateAppEndpoint_AppFound_BodyWithDisplayText_Should_Return_Ok()
    {
        // Arrange
        const string newDisplayText = "newDisplayText";
        
        var client = _factory.CreateClient();
        var body = new UpdateAppBody
        {
            DisplayText = newDisplayText,
        };
        
        // Act
        var createRes = await client.PostAsJsonAsync("strings/api/v1/apps", new CreateAppBody
        {
            DisplayText = "Test"
        });
        var app = await createRes.Content.ReadFromJsonAsync<App>();
        
        var response = await client.PutAsJsonAsync($"strings/api/v1/apps/{app!.Id}", body);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        
        var data = await response.Content.ReadFromJsonAsync<App>();
        data!.DisplayText.ShouldBe(newDisplayText);
    }
    
    [Fact]
    public async Task DeleteAppEndpoint_AppNotFound_Should_Return_DeletedResult_Zero()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.DeleteAsync("strings/api/v1/apps/0");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        var data = await response.Content.ReadFromJsonAsync<DeletedResult>();
        data!.Count.ShouldBe(0);
    }
    
   [Fact]
    public async Task DeleteAppEndpoint_AppFound_Should_Return_DeletedResult_One()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var createRes = await client.PostAsJsonAsync("strings/api/v1/apps", new CreateAppBody
        {
            DisplayText = "Test"
        });
        var app = await createRes.Content.ReadFromJsonAsync<App>();
        
        var response = await client.DeleteAsync($"strings/api/v1/apps/{app!.Id}");
        var updateRes = await client.PutAsJsonAsync($"strings/api/v1/apps/{app.Id}", new UpdateAppBody());
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        updateRes.StatusCode.ShouldBe(HttpStatusCode.UnprocessableEntity);
        
        var data = await response.Content.ReadFromJsonAsync<DeletedResult>();
        data!.Count.ShouldBe(1);
        
        var updateData = await updateRes.Content.ReadFromJsonAsync<ErrorCodeResult>();
        updateData!.Code.ShouldBe("NotFound");
    }
}
