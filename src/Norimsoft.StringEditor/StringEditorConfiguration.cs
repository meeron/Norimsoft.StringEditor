namespace Norimsoft.StringEditor;

public class StringEditorConfiguration
{
    internal StringEditorConfiguration()
    {
    }
    
    public string Path { get; set; } = "/strings";

    public bool RunMigration { get; set; } = true;
}