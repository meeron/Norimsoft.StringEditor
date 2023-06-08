using Norimsoft.StringEditor.DataProvider;

namespace Norimsoft.StringEditor.Endpoints.Apps;

internal static class DeleteAppEndpoint
{
    internal static async Task<IResult> Handler(
        int id,
        [FromServices] IDataContext dataContext) =>
        Results.Ok(new DeletedResult(await dataContext.Apps.Delete(id, CancellationToken.None)));
}
