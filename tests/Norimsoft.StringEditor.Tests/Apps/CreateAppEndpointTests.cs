using Microsoft.AspNetCore.Http.HttpResults;
using Norimsoft.StringEditor.DataProvider.Models;
using Norimsoft.StringEditor.Endpoints.Apps;
using Norimsoft.StringEditor.Endpoints.Apps.Models;
using NSubstitute;
using Shouldly;
using Slugify;

namespace Norimsoft.StringEditor.Tests.Apps;

public class CreateAppEndpointTests : AppEndpointTests
{
    private readonly ISlugHelper _mockSlugHelper;

    public CreateAppEndpointTests()
    {
        _mockSlugHelper = Substitute.For<ISlugHelper>();
    }

    [Fact]
    public async Task DisplayText_IsEmpty_ShouldReturnBadRequest()
    {
        // Arrange
        var body = new CreateAppBody();
        var validator = new CreateAppBody.Validator();
        
        // Act
        var result = await CreateAppEndpoint.Handler(body, validator, MockDbContext, _mockSlugHelper);
        var badRequest = result as BadRequest<ErrorResult>;
        
        // Assert
        badRequest.ShouldNotBeNull();
        badRequest.Value?.Message.ShouldBe("'Display Text' must not be empty.");
    }
    
    [Fact]
    public async Task DisplayText_HasValue_ShouldReturnCreatedResult()
    {
        // Arrange
        const string displayText = "obi-wan kenobi";
        const string slug = "slug";
        
        var validator = new CreateAppBody.Validator();
        var body = new CreateAppBody
        {
            DisplayText = displayText,
        };

        _mockSlugHelper.GenerateSlug(displayText).Returns(slug);
        MockAppRepository.Insert(Arg.Is<App>(a => a.DisplayText == displayText && a.Slug == slug),
                CancellationToken.None)
            .Returns(new App { Id = 1 });
        
        // Act
        var result = await CreateAppEndpoint.Handler(body, validator, MockDbContext, _mockSlugHelper);
        var created = result as Created<App>;
        
        // Assert
        created.ShouldNotBeNull();
        created.Location.ShouldBe("/apps/1");
    }
}
