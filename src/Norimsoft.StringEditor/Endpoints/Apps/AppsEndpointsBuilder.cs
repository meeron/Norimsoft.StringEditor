using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Norimsoft.StringEditor.Endpoints.Apps;

internal static class AppsEndpointsBuilder
{
    internal static void MapAppsEndpoints(this RouteGroupBuilder mainGroup)
    {
        var appGroup = mainGroup.MapGroup("api/v1/apps");
        
        appGroup.MapGet("", GetAppsEndpoint.Handler);
        appGroup.MapPost("", CreateAppEndpoint.Handler);
        appGroup.MapDelete("{id:int}", DeleteAppEndpoint.Handler);
        appGroup.MapPut("{id:int}", UpdateAppEndpoint.Handler);
    }
}
