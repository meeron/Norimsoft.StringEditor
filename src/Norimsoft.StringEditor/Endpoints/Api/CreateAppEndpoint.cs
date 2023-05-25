using Norimsoft.StringEditor.DataProvider;
using Norimsoft.StringEditor.DataProvider.Models;
using Norimsoft.StringEditor.Endpoints.Api.Models;
using Slugify;

namespace Norimsoft.StringEditor.Endpoints.Api;

internal static class CreateAppEndpoint
{
    internal static async Task<IResult> Handler(
        [FromBody] CreateAppBody? body,
        [FromServices] IStringEditorDataProvider dataProvider,
        [FromServices] ISlugHelper slugHelper)
    {
        if (string.IsNullOrWhiteSpace(body?.DisplayText))
        {
            return Results.BadRequest(new { Message = "'displayText' is required" });
        }

        var newApp = new App
        {
            Slug = slugHelper.GenerateSlug(body.DisplayText),
            DisplayText = body.DisplayText,
        };

        newApp = await dataProvider.InsertApp(newApp, CancellationToken.None);

        return Results.Created($"/apps/{newApp.Id}", newApp);
    }
}
