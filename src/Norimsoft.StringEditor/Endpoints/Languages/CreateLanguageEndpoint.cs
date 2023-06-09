using Norimsoft.StringEditor.DataProvider;
using Norimsoft.StringEditor.DataProvider.Models;
using Norimsoft.StringEditor.Endpoints.Languages.Models;

namespace Norimsoft.StringEditor.Endpoints.Languages;

internal static class CreateLanguageEndpoint
{
    internal static async Task<IResult> Handler(
        [FromBody] CreateLanguageBody? body,
        [FromServices] IDataContext dataContext)
    {
        if (string.IsNullOrWhiteSpace(body?.Code))
        {
            return ErrorResults.BadRequest("'code' is required");
        }
        
        if (string.IsNullOrWhiteSpace(body.EnglishName))
        {
            return ErrorResults.BadRequest("'englishName' is required");
        }
        
        if (string.IsNullOrWhiteSpace(body.NativeName))
        {
            return ErrorResults.BadRequest("'nativeName' is required");
        }

        var newLang = await dataContext.Languages.Insert(new Language
        {
            Code = body.Code,
            NativeName = body.NativeName,
            EnglishName = body.EnglishName,
        }, CancellationToken.None);

        return Results.Created($"/languages/{newLang.Id}", newLang);
    }
}
