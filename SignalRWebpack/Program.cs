using SignalRWebpack.Hubs;
using System.Text.Json;
using System.Text;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SignalRWebpack.Support;
using System.Net;

// ***** Initialize Web App *****
var builder = WebApplication.CreateBuilder(args);

// ***** Configure for OAuth *****

builder.Services.ConfigureSameSiteNoneCookies(); // for http support
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"] ?? "";
    options.ClientId = builder.Configuration["Auth0:ClientId"] ?? "";
});

// ***** Configure SignalR and Controllers *****
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

// ***** Build + Middleware *****
var app = builder.Build();
var env = app.Environment;
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// ***** Configure Controllers (Using Traditional MVC) *****
#pragma warning disable ASP0014
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});
#pragma warning restore ASP0014

// ***** SignalR *****
app.MapHub<ChatHub>("/hub");

app.Run();
