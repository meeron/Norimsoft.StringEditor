namespace Norimsoft.StringEditor.Endpoints.Languages;

internal static class UpdateLanguageEndpoint
{
    internal static async Task<IResult> Handler()
    {
        await Task.CompletedTask;

        return Results.Ok();
    }
}
