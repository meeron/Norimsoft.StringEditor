namespace Norimsoft.StringEditor.Extensions;

internal static class HttpContextExtensions
{
    private const string ConfigItemKey = "Norimsoft.StringEditor.Config";
    
    internal static void SetConfig(this HttpContext ctx, StringEditorConfiguration config) =>
        ctx.Items[ConfigItemKey] = config;

    internal static StringEditorConfiguration GetConfig(this HttpContext ctx) =>
        (StringEditorConfiguration)ctx.Items[ConfigItemKey]!;
}
