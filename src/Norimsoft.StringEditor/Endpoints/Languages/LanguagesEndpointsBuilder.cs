using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Norimsoft.StringEditor.Endpoints.Languages;

internal static class LanguagesEndpointsBuilder
{
    public static void MapLanguagesEndpoints(this RouteGroupBuilder mainGroup)
    {
        var group = mainGroup.MapGroup("api/v1/langs");

        group.MapGet("", GetLanguagesEndpoint.Handler);
        group.MapPost("", CreateLanguageEndpoint.Handler);
        group.MapPut("{id:int}", UpdateLanguageEndpoint.Handler);
        group.MapDelete("{id:int}", DeleteLanguageEndpoint.Handler);
    }
}
