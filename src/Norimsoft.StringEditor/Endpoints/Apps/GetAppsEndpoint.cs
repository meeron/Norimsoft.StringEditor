using Norimsoft.StringEditor.DataProvider;

namespace Norimsoft.StringEditor.Endpoints.Apps;

internal static class GetAppsEndpoint
{
    internal static async Task<IResult> Handler(
        [FromServices] IStringEditorDataProvider dataProvider) =>
        Results.Ok(await dataProvider.GetApps(CancellationToken.None));
}
