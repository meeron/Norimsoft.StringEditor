global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using MiddlewareHandler = System.Func<Microsoft.AspNetCore.Http.HttpContext, Microsoft.AspNetCore.Http.RequestDelegate, System.Threading.Tasks.Task>;

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Norimsoft.StringEditor.Tests")]
