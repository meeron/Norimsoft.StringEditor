using Norimsoft.StringEditor.DataProvider;
using Norimsoft.StringEditor.Endpoints.Apps.Models;

namespace Norimsoft.StringEditor.Endpoints.Apps;

internal static class UpdateAppEndpoint
{
    internal static async Task<IResult> Handler(
        [FromRoute] int id,
        [FromBody] UpdateAppBody? body,
        [FromServices] IDataContext dataContext)
    {
        var app = await dataContext.Apps.GetApp(id, CancellationToken.None);
        if (app == null)
        {
            return Results.UnprocessableEntity(new
            {
                Code = "NotFound",
            });
        }

        if (!string.IsNullOrWhiteSpace(body?.Slug))
        {
            app.Slug = body.Slug;
        }

        if (!string.IsNullOrWhiteSpace(body?.DisplayText))
        {
            app.DisplayText = body.DisplayText;
        }

        if (await dataContext.Apps.UpdateApp(app, CancellationToken.None) == 0)
        {
            return Results.UnprocessableEntity(new
            {
                Code = "NoChange",
            });
        }

        return Results.Ok(await dataContext.Apps.GetApp(app.Id, CancellationToken.None));
    }
}
