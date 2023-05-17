using System.Reflection;

namespace Norimsoft.StringEditor.Helpers;

public static class EmbeddedHelpers
{
    public static Stream? GetResource(string path)
    {
        var resourceName = $"Norimsoft.StringEditor.wwwroot.{path.Replace("/", ".")}";
        
        var asm = Assembly.GetExecutingAssembly();

        return asm.GetManifestResourceStream(resourceName);
    }
}