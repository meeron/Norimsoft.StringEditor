using FluentValidation;

namespace Norimsoft.StringEditor.Endpoints.Languages.Models;

public class CreateLanguageBody
{
    public string Code { get; set; } = "";

    public string EnglishName { get; set; } = "";

    public string NativeName { get; set; } = "";

    public class Validator : AbstractValidator<CreateLanguageBody>
    {
        public Validator()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.EnglishName).NotEmpty();
            RuleFor(x => x.NativeName).NotEmpty();
        }
    }
}
