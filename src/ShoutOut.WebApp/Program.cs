using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using ShoutOut.Database;
using ShoutOut.WebApp.Authentication;
using ShoutOut.WebApp.Components;
using ShoutOut.WebApp.Cryptography;
using ShoutOut.WebApp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMudServices();
// Add services to the container.

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<ShoutOutDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("MainDb")));

builder.Services.AddControllers();
builder.Services.AddScoped<PostsService>();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<CustomAuthenticationStateProvider>(sp =>
    (CustomAuthenticationStateProvider)sp.GetRequiredService<AuthenticationStateProvider>());
builder.Services.AddTransient<IHashingService, HashingService>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<RegistrationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();