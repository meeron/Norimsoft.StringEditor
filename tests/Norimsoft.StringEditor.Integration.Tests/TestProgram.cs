using Norimsoft.StringEditor;
using Norimsoft.StringEditor.DataProvider.SqlServer;

var builder = WebApplication.CreateBuilder();

builder.Services.AddStringEditor(o =>
{
    o.UseSqlServer("Server=localhost;Database=StringEditor_Tests;User Id=sa;Password=Mn9-hwL8J;");
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
