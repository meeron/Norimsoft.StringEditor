using Norimsoft.StringEditor.DataProvider;

namespace Norimsoft.StringEditor.Endpoints.Languages;

internal static class DeleteLanguageEndpoint
{
    internal static async Task<IResult> Handler(
        int id,
        [FromServices] IDataContext dataContext)
    {
        var deleted = await dataContext.Languages.Delete(id, CancellationToken.None);
        
        return Results.Ok(new DeletedResult(deleted));
    }
}
