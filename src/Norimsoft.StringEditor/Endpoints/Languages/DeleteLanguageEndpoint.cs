namespace Norimsoft.StringEditor.Endpoints.Languages;

internal static class DeleteLanguageEndpoint
{
    internal static async Task<IResult> Handler()
    {
        await Task.CompletedTask;

        return Results.Ok();
    }
}
