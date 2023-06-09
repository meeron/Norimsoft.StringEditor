using FluentValidation;
using Norimsoft.StringEditor.DataProvider;
using Norimsoft.StringEditor.DataProvider.Models;
using Norimsoft.StringEditor.Endpoints.Languages.Models;
using Norimsoft.StringEditor.Extensions;

namespace Norimsoft.StringEditor.Endpoints.Languages;

internal static class CreateLanguageEndpoint
{
    internal static async Task<IResult> Handler(
        [FromBody] CreateLanguageBody body,
        [FromServices] IDataContext dataContext,
        [FromServices] IValidator<CreateLanguageBody> validator)
    {
        var validationResult = await validator.ValidateAsync(body);
        if (!validationResult.IsValid)
        {
            return validationResult.AsBadRequestResult();
        }

        var newLang = await dataContext.Languages.Insert(new Language
        {
            Code = body.Code,
            NativeName = body.NativeName,
            EnglishName = body.EnglishName,
        }, CancellationToken.None);

        return Results.Created($"/languages/{newLang.Id}", newLang);
    }
}
