using Norimsoft.StringEditor.Helpers;

namespace Norimsoft.StringEditor.Endpoints;

internal static class HomeEndpoint
{
    internal static IResult Handler()
    {
        var dataStream = EmbeddedHelpers.GetResource("index.html");

        return dataStream != null
            ? Results.File(dataStream, "text/html")
            : Results.NotFound();
    }
}
