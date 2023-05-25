namespace Norimsoft.StringEditor.Endpoints;

internal static class GetStringsEndpoint
{
    internal static IResult Handler(string env, string appName)
    {
        return Results.Ok(new { env, appName });
    }
}
