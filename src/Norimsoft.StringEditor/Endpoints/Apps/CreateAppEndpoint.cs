using Norimsoft.StringEditor.DataProvider;
using Norimsoft.StringEditor.DataProvider.Models;
using Norimsoft.StringEditor.Endpoints.Apps.Models;
using Slugify;

namespace Norimsoft.StringEditor.Endpoints.Apps;

internal static class CreateAppEndpoint
{
    internal static async Task<IResult> Handler(
        [FromBody] CreateAppBody? body,
        [FromServices] IDataContext dataContext,
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

        newApp = await dataContext.Apps.InsertApp(newApp, CancellationToken.None);

        return Results.Created($"/apps/{newApp.Id}", newApp);
    }
}
