namespace Norimsoft.StringEditor.DataProvider.Models;

public class Release : Entity
{
    public string Version { get; set; } = "";
    
    public DateTime CreatedAtUtc { get; set; }
}
