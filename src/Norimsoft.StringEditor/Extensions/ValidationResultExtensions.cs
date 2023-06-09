using FluentValidation.Results;

namespace Norimsoft.StringEditor.Extensions;

internal static class ValidationResultExtensions
{
    internal static IResult AsBadRequestResult(this ValidationResult validationResult) =>
        ErrorResults.BadRequest(validationResult.Errors[0].ErrorMessage);
}
