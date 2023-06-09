using FluentValidation;

namespace Norimsoft.StringEditor.Endpoints.Apps.Models;

public class CreateAppBody
{
    public string DisplayText { get; set; } = "";

    public class Validator : AbstractValidator<CreateAppBody>
    {
        public Validator()
        {
            RuleFor(x => x.DisplayText).NotEmpty();
        }
    }
}
