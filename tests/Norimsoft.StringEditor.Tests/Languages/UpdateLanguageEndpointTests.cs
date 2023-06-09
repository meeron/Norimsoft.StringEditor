using Microsoft.AspNetCore.Http.HttpResults;
using Norimsoft.StringEditor.DataProvider.Models;
using Norimsoft.StringEditor.Endpoints.Languages;
using Norimsoft.StringEditor.Endpoints.Languages.Models;
using NSubstitute;
using Shouldly;

namespace Norimsoft.StringEditor.Tests.Languages;

public class UpdateLanguageEndpointTests : LanguagesEndpointTests
{
    [Fact]
    public async Task LangNotFound_ShouldReturnErrorCodeResult()
    {
        // Arrange
        var body = new UpdateLanguageBody();
        
        // Act
        var result = await UpdateLanguageEndpoint.Handler(0, body, MockDbContext);
        var unprocessableEntity = result as UnprocessableEntity<ErrorCodeResult>;
        
        // Assert
        unprocessableEntity.ShouldNotBeNull();
        unprocessableEntity.Value?.Code.ShouldBe("NotFound");
    }

    [Fact]
    public async Task NothingToUpdate_ShouldReturnErrorCodeResult()
    {
        // Arrange
        const int id = 666;
        var body = new UpdateLanguageBody();

        MockRepository.Get(id, CancellationToken.None).Returns(new Language());
        
        // Act
        var result = await UpdateLanguageEndpoint.Handler(id, body, MockDbContext);
        var unprocessableEntity = result as UnprocessableEntity<ErrorCodeResult>;
        
        // Assert
        unprocessableEntity.ShouldNotBeNull();
        unprocessableEntity.Value?.Code.ShouldBe("NoChange");
    }

    [Fact]
    public async Task GivenEnglishName_ShouldUpdateAndReturnOk()
    {
        // Arrange
        const int id = 666;
        var body = new UpdateLanguageBody
        {
            EnglishName = "name",
        };
        
        MockRepository.Get(id, CancellationToken.None).Returns(new Language());
        MockRepository.Update(
                Arg.Is<Language>(x => x.EnglishName == body.EnglishName),
                CancellationToken.None)
            .Returns(1);
        
        // Act
        var result = await UpdateLanguageEndpoint.Handler(id, body, MockDbContext);
        var ok = result as Ok<Language>;
        
        // Assert
        ok.ShouldNotBeNull();
    }
    
    [Fact]
    public async Task GivenNativeName_ShouldUpdateAndReturnOk()
    {
        // Arrange
        const int id = 666;
        var body = new UpdateLanguageBody
        {
            NativeName = "name",
        };
        
        MockRepository.Get(id, CancellationToken.None).Returns(new Language());
        MockRepository.Update(
                Arg.Is<Language>(x => x.NativeName == body.NativeName),
                CancellationToken.None)
            .Returns(1);
        
        // Act
        var result = await UpdateLanguageEndpoint.Handler(id, body, MockDbContext);
        var ok = result as Ok<Language>;
        
        // Assert
        ok.ShouldNotBeNull();
    }
}