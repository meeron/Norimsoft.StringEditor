namespace Norimsoft.StringEditor.DataProvider.Models;

public class Environment : Entity
{
    public int AppFk { get; set; }

    public string Slug { get; set; } = "";

    public string DisplayText { get; set; } = "";
}
