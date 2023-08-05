using Microsoft.AspNetCore.Http.HttpResults;
using Norimsoft.StringEditor.DataProvider.Models;
using Norimsoft.StringEditor.Endpoints.Apps;
using Norimsoft.StringEditor.Endpoints.Apps.Models;
using NSubstitute;
using Shouldly;

namespace Norimsoft.StringEditor.Tests.Apps;

public class UpdateAppEndpointTests : AppEndpointTests
{
    [Fact]
    public async Task AppNotFound_ShouldReturnErrorCodeResult()
    {
        // Arrange
        var body = new UpdateAppBody();
        
        // Act
        var result = await UpdateAppEndpoint.Handler(0, body, MockDbContext);
        var unprocessableEntity = result as UnprocessableEntity<ErrorCodeResult>;
        
        // Assert
        unprocessableEntity.ShouldNotBeNull();
        unprocessableEntity.Value?.Code.ShouldBe("NotFound");
    }

    [Fact]
    public async Task WithSlug_ShouldUpdateAppAndReturnOkResult()
    {
        // Arrange
        const int id = 666;
        const string slug = "new-slug";
        
        var app = new App
        {
            Id = id,
        };
        var body = new UpdateAppBody
        {
            Slug = slug,
        };

        MockAppRepository.Get(id, CancellationToken.None).Returns(app);
        MockAppRepository.Update(Arg.Is<App>(a => a.Slug == slug), CancellationToken.None).Returns(1);
        
        // Act
        var result = await UpdateAppEndpoint.Handler(id, body, MockDbContext);
        var ok = result as Ok<App>;
        
        // Assert
        ok.ShouldNotBeNull();
    }
    
    [Fact]
    public async Task WithDisplayText_ShouldUpdateAppAndReturnOkResult()
    {
        // Arrange
        const int id = 666;
        const string displayText = "Display Text";
        
        var app = new App
        {
            Id = id,
        };
        var body = new UpdateAppBody
        {
            DisplayText = displayText,
        };

        MockAppRepository.Get(id, CancellationToken.None).Returns(app);
        MockAppRepository.Update(Arg.Is<App>(a => a.DisplayText == displayText), CancellationToken.None).Returns(1);
        
        // Act
        var result = await UpdateAppEndpoint.Handler(id, body, MockDbContext);
        var ok = result as Ok<App>;
        
        // Assert
        ok.ShouldNotBeNull();
    }
}
