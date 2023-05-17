namespace Norimsoft.StringEditor.DataProvider.Models;

public class StringText
{
    public int Id { get; set; }
    
    public int StringKeyFk { get; set; }
    
    public int LanguageFk { get; set; }
    
    public int Version { get; set; }

    public string Text { get; set; } = "";
    
    public bool IsDeleted { get; set; }
}