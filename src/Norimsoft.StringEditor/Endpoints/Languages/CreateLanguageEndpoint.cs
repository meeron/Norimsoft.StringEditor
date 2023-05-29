namespace Norimsoft.StringEditor.Endpoints.Languages;

internal static class CreateLanguageEndpoint
{
    internal static async Task<IResult> Handler()
    {
        await Task.CompletedTask;

        return Results.Ok();
    }
}
