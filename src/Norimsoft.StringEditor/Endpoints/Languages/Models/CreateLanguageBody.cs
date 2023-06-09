namespace Norimsoft.StringEditor.Endpoints.Languages.Models;

public class CreateLanguageBody
{
    public string Code { get; set; } = "";

    public string EnglishName { get; set; } = "";

    public string NativeName { get; set; } = "";
}