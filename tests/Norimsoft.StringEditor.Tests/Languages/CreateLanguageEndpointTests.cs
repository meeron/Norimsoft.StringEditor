using Microsoft.AspNetCore.Http.HttpResults;
using Norimsoft.StringEditor.DataProvider.Models;
using Norimsoft.StringEditor.Endpoints.Languages;
using Norimsoft.StringEditor.Endpoints.Languages.Models;
using NSubstitute;
using Shouldly;

namespace Norimsoft.StringEditor.Tests.Languages;

public class CreateLanguageEndpointTests : LanguagesEndpointTests
{
    [Fact]
    public async Task CodeIsEmpty_ShouldReturnBadRequestResult()
    {
        // Arrange
        var body = new CreateLanguageBody();
        
        // Act
        var result = await CreateLanguageEndpoint.Handler(body, MockDbContext);
        var badRequest = result as BadRequest<ErrorResult>;
        
        // Assert
        badRequest.ShouldNotBeNull();
        badRequest.Value?.Message.ShouldBe("'code' is required");
    }
    
    [Fact]
    public async Task EnglishNameIsEmpty_ShouldReturnBadRequestResult()
    {
        // Arrange
        var body = new CreateLanguageBody
        {
            Code = "code",
        };
        
        // Act
        var result = await CreateLanguageEndpoint.Handler(body, MockDbContext);
        var badRequest = result as BadRequest<ErrorResult>;
        
        // Assert
        badRequest.ShouldNotBeNull();
        badRequest.Value?.Message.ShouldBe("'englishName' is required");
    }
    
    [Fact]
    public async Task NativeNameIsEmpty_ShouldReturnBadRequestResult()
    {
        // Arrange
        var body = new CreateLanguageBody
        {
            Code = "code",
            EnglishName = "name",
        };
        
        // Act
        var result = await CreateLanguageEndpoint.Handler(body, MockDbContext);
        var badRequest = result as BadRequest<ErrorResult>;
        
        // Assert
        badRequest.ShouldNotBeNull();
        badRequest.Value?.Message.ShouldBe("'nativeName' is required");
    }
    
    [Fact]
    public async Task BodyIsValid_ShouldReturnCreatedResult()
    {
        // Arrange
        var body = new CreateLanguageBody
        {
            Code = "code",
            EnglishName = "name",
            NativeName = "name"
        };

        MockRepository.Insert(Arg.Is<Language>(x => x.Code == body.Code
                                                    && x.EnglishName == body.EnglishName
                                                    && x.NativeName == body.NativeName), CancellationToken.None)
            .Returns(new Language { Id = 1 });
        
        // Act
        var result = await CreateLanguageEndpoint.Handler(body, MockDbContext);
        var created = result as Created<Language>;
        
        // Assert
        created.ShouldNotBeNull();
        created.Location.ShouldBe("/languages/1");
    }
}