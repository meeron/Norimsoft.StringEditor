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
        
        var config = ctx.GetConfig();

        if (!string.IsNullOrWhiteSpace(config.WebRootPath)
            && Directory.Exists(config.WebRootPath))
        {
            var indexPath = Path.Join(config.WebRootPath, "index.html");
        
            return File.Exists(indexPath)
                ? StreamResult(File.OpenRead(indexPath), config)
                : Results.NoContent();
        }

        var dataStream = EmbeddedHelpers.GetResource("index.html");

        return dataStream != null
            ? StreamResult(dataStream, config)
            : Results.NoContent();
    }

    private static IResult StreamResult(Stream dataStream, StringEditorConfiguration config)
    {
        using var reader = new StreamReader(dataStream);

        var content = reader.ReadToEnd();

        // Replace base path for static files with the current configured one
        _indexCache = content.Replace("/strings/", $"{config.Path}/");

        return Results.Content(_indexCache, "text/html");
    }
}
