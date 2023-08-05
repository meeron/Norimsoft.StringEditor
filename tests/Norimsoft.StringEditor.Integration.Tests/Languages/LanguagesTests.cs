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

    [Fact]
    public async Task DeleteLanguageEndpoint_LanguageIsDeleted_Should_ReturnOkWithDeletedResult()
    {
        // Arrange
        var client = _factory.CreateClient();
        var body = new CreateLanguageBody
        {
            Code = "pl",
            NativeName = "test",
            EnglishName = "test"
        };
        
        var createdRes = await client.PostAsJsonAsync("strings/api/v1/langs", body);
        var lang = await createdRes.Content.ReadFromJsonAsync<Language>();

        // Act
        var response = await client.DeleteAsync($"strings/api/v1/langs/{lang!.Id}");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        
        var data = await response.Content.ReadFromJsonAsync<DeletedResult>();
        data!.Count.ShouldBe(1);
    }
    
    [Fact]
    public async Task DeleteLanguageEndpoint_LanguageNotFound_Should_ReturnOkWithDeletedResultZero()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.DeleteAsync("strings/api/v1/langs/666");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        
        var data = await response.Content.ReadFromJsonAsync<DeletedResult>();
        data!.Count.ShouldBe(0);
    }

    [Fact]
    public async Task GetLanguagesEndpoint_Should_ReturnOk()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.GetAsync("strings/api/v1/langs");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        var data = await response.Content.ReadFromJsonAsync<Language[]>();
        data!.ShouldBeEmpty();
    }

    [Fact]
    public async Task UpdateLanguageEndpoint_LanguageNotFound_Should_Return_NotFoundCode()
    {
        // Arrange
        var client = _factory.CreateClient();
        var body = new UpdateLanguageBody();
        
        // Act
        var response = await client.PutAsJsonAsync("strings/api/v1/langs/666", body);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.UnprocessableEntity);
        
        var data = await response.Content.ReadFromJsonAsync<ErrorCodeResult>();
        data!.Code.ShouldBe("NotFound");
    }
    
    [Fact]
    public async Task UpdateLanguageEndpoint_WithBody_Should_Return_Ok()
    {
        // Arrange
        const string newNativeName = "newNativeName";
        const string newEnglishName = "newEnglishName";
        
        var client = _factory.CreateClient();
        var updateBody = new UpdateLanguageBody
        {
            EnglishName = newEnglishName,
            NativeName = newNativeName,
        };
        var createBody = new CreateLanguageBody
        {
            Code = "pl",
            NativeName = "test",
            EnglishName = "test"
        };
        
        var createdRes = await client.PostAsJsonAsync("strings/api/v1/langs", createBody);
        var lang = await createdRes.Content.ReadFromJsonAsync<Language>();
        
        // Act
        var response = await client.PutAsJsonAsync($"strings/api/v1/langs/{lang!.Id}", updateBody);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        
        var data = await response.Content.ReadFromJsonAsync<Language>();
        data!.EnglishName.ShouldBe(newEnglishName);
        data!.NativeName.ShouldBe(newNativeName);
    }
}
