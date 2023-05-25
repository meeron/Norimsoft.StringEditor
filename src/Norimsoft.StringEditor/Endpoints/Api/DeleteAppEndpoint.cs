using Norimsoft.StringEditor.DataProvider;

namespace Norimsoft.StringEditor.Endpoints.Api;

internal static class DeleteAppEndpoint
{
    internal static async Task<IResult> Handler(
        int id,
        [FromServices] IStringEditorDataProvider dataProvider) =>
        Results.Ok(new
        {
            Count = await dataProvider.DeleteApp(id, CancellationToken.None),
        });
}
