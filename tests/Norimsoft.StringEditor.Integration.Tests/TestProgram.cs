using Norimsoft.StringEditor;
using Norimsoft.StringEditor.DataProvider.SqlServer;

var builder = WebApplication.CreateBuilder();

await Helpers.InitDb();

builder.Services.AddStringEditor(o =>
{
    o.UseSqlServer(Helpers.DbConnectionStringData);
});

var app = builder.Build();

var path = app.Environment.ContentRootPath
    .Replace("/bin/Debug/net7.0", "/wwwroot");

app.UseStringEditor(config =>
{
    config.WebRootPath = path;
});
app.UseRouting();

app.Run();

public partial class TestProgram { }
