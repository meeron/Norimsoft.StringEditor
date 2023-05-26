using Norimsoft.StringEditor.Extensions;

namespace Norimsoft.StringEditor.Middlewares;

public class StringEditorConfigurationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly StringEditorConfiguration _config;

    public StringEditorConfigurationMiddleware(RequestDelegate next, StringEditorConfiguration config)
    {
        _next = next;
        _config = config;
    }

    public Task InvokeAsync(HttpContext ctx)
    {
        ctx.SetConfig(_config);
        return _next(ctx);
    }
}
