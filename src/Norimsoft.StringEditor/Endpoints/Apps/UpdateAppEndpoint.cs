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
        var app = await dataContext.Apps.Get(id, CancellationToken.None);
        if (app == null)
        {
            return ErrorResults.NotFound();
        }

        if (!string.IsNullOrWhiteSpace(body?.Slug))
        {
            app.Slug = body.Slug;
        }

        if (!string.IsNullOrWhiteSpace(body?.DisplayText))
        {
            app.DisplayText = body.DisplayText;
        }

        await dataContext.Apps.Update(app, CancellationToken.None);

        return Results.Ok(await dataContext.Apps.Get(app.Id, CancellationToken.None));
    }
}
