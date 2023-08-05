using Norimsoft.StringEditor.DataProvider.Models;
using Norimsoft.StringEditor.Endpoints.Languages.Models;

namespace Norimsoft.StringEditor.Integration.Tests.Languages;

public class LanguagesTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;

    public LanguagesTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task CreateLanguageEndpoint_EmptyBody_Should_ReturnBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.PostAsJsonAsync("strings/api/v1/langs", new CreateLanguageBody());
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task CreateLanguageEndpoint_EmptyBody_Should_ReturnCreated()
    {
        // Arrange
        var client = _factory.CreateClient();
        var body = new CreateLanguageBody
        {
            Code = "pl",
            NativeName = "test",
            EnglishName = "test"
        };

        // Act
        var response = await client.PostAsJsonAsync("strings/api/v1/langs", body);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        
        var data = await response.Content.ReadFromJsonAsync<Language>();
        var location = response.Headers.Location?.ToString();
        
        location!.ShouldBe($"/languages/{data!.Id}");
    }
}
