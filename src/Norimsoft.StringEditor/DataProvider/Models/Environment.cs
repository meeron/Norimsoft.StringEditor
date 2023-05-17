namespace Norimsoft.StringEditor.DataProvider.Models;

public class Environment
{
    public int Id { get; set; }
    
    public int AppFk { get; set; }

    public string Slug { get; set; } = "";

    public string DisplayText { get; set; } = "";
}