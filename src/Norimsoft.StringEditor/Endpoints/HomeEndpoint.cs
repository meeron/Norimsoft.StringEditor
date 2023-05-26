using Norimsoft.StringEditor.Extensions;
using Norimsoft.StringEditor.Helpers;

namespace Norimsoft.StringEditor.Endpoints;

internal static class HomeEndpoint
{
    private static string _indexCache = "";
    
    internal static IResult Handler(HttpContext ctx)
    {
        if (_indexCache != "")
        {
            return Results.Content(_indexCache, "text/html");
        }
        
        var dataStream = EmbeddedHelpers.GetResource("index.html");
        if (dataStream == null)
        {
            return Results.NotFound();
        }

        var config = ctx.GetConfig();
        using var reader = new StreamReader(dataStream);

        // Replace base path for static files with the current configured one
        _indexCache = reader.ReadToEnd().Replace("/strings/", $"{config.Path}/");

        return Results.Content(_indexCache, "text/html");
    }
}
