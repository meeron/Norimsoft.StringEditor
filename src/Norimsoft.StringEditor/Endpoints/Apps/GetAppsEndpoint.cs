using Norimsoft.StringEditor.DataProvider;

namespace Norimsoft.StringEditor.Endpoints.Apps;

internal static class GetAppsEndpoint
{
    internal static async Task<IResult> Handler(
        [FromServices] IDataContext dataContext) =>
        Results.Ok(await dataContext.Apps.GetApps(CancellationToken.None));
}
