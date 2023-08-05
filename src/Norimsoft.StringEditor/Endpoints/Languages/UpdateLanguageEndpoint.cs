using Norimsoft.StringEditor.DataProvider;
using Norimsoft.StringEditor.Endpoints.Languages.Models;

namespace Norimsoft.StringEditor.Endpoints.Languages;

internal static class UpdateLanguageEndpoint
{
    internal static async Task<IResult> Handler(
        int id,
        [FromBody] UpdateLanguageBody body,
        [FromServices] IDataContext dataContext)
    {
        var entity = await dataContext.Languages.Get(id, CancellationToken.None);
        if (entity == null)
        {
            return ErrorResults.NotFound();
        }

        if (!string.IsNullOrWhiteSpace(body.EnglishName))
        {
            entity.EnglishName = body.EnglishName;
        }
        
        if (!string.IsNullOrWhiteSpace(body.NativeName))
        {
            entity.NativeName = body.NativeName;
        }

        await dataContext.Languages.Update(entity, CancellationToken.None);

        return Results.Ok(await dataContext.Languages.Get(id, CancellationToken.None));
    }
}
