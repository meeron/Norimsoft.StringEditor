namespace Norimsoft.StringEditor;

public class StringEditorConfiguration
{
    internal StringEditorConfiguration()
    {
    }

    public PathString Path { get; set; } = "/strings";

    public bool RunMigration { get; set; } = true;
}
