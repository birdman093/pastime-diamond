// <snippet_HubsNamespace>
using SignalRWebpack.Hubs;
using System.Text.Json;
using System.Text;
// </snippet_HubsNamespace>

// <snippet_AddSignalR>
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
// </snippet_AddSignalR>

// <snippet_FilesMiddleware>
var app = builder.Build();
var env = app.Environment;

app.UseDefaultFiles();
app.UseStaticFiles();
// </snippet_FilesMiddleware>

// <snippet_MapHub>
app.MapHub<ChatHub>("/hub");
// </snippet_MapHub>

// Map "/login" to login.html
app.MapGet("/", async context =>
{
    context.Response.ContentType = "text/html";
    await context.Response.SendFileAsync(Path.Combine(env.WebRootPath, "login.html"));
});

app.MapGet("/chat/{chatId}", async context =>
{
    context.Response.ContentType = "text/html";
    await context.Response.SendFileAsync(Path.Combine(env.WebRootPath, "chat.html"));
});


app.MapPost("/check-login", async (HttpContext context) =>
{
    using var reader = new StreamReader(context.Request.Body, Encoding.UTF8);
    var body = await reader.ReadToEndAsync();
    var data = JsonSerializer.Deserialize<Dictionary<string, string>>(body);

    if (data != null && data.ContainsKey("username") && data["username"] == "YES")
    {
        var chatId = 1111111; // for testing only

        context.Response.Redirect($"/chat/{chatId}");
    }
    else
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
    }
});


app.Run();
