namespace Norimsoft.StringEditor.DataProvider.Models;

public class Release
{
    public int Id { get; set; }

    public string Version { get; set; } = "";
    
    public DateTime CreatedAtUtc { get; set; }
}
