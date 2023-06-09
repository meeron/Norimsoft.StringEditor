using FluentValidation;
using Norimsoft.StringEditor.DataProvider;
using Norimsoft.StringEditor.DataProvider.Models;
using Norimsoft.StringEditor.Endpoints.Apps.Models;
using Norimsoft.StringEditor.Extensions;
using Slugify;

namespace Norimsoft.StringEditor.Endpoints.Apps;

internal static class CreateAppEndpoint
{
    internal static async Task<IResult> Handler(
        [FromBody] CreateAppBody body,
        [FromServices] IValidator<CreateAppBody> validator,
        [FromServices] IDataContext dataContext,
        [FromServices] ISlugHelper slugHelper)
    {
        var validationResult = await validator.ValidateAsync(body);
        if (!validationResult.IsValid)
        {
            return validationResult.AsBadRequestResult();
        }

        var newApp = new App
        {
            Slug = slugHelper.GenerateSlug(body.DisplayText),
            DisplayText = body.DisplayText,
        };

        newApp = await dataContext.Apps.Insert(newApp, CancellationToken.None);

        return Results.Created($"/apps/{newApp.Id}", newApp);
    }
}
