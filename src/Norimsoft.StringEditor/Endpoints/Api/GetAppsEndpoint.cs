namespace Norimsoft.StringEditor.Endpoints.Api;

internal static class GetAppsEndpoint
{
    internal static async Task<IResult> Handler()
    {
        await Task.CompletedTask;

        return Results.Ok(new { });
    }
}
