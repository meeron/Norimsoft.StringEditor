using Norimsoft.StringEditor.DataProvider;

namespace Norimsoft.StringEditor.Endpoints.Languages;

internal static class GetLanguagesEndpoint
{
    internal static async Task<IResult> Handler(
        [FromServices] IDataContext dataContext)
    {
        var entities = await dataContext.Languages.Get(CancellationToken.None);

        return Results.Ok(entities);
    }
}
