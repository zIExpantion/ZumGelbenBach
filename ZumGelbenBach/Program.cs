using ZumGelbenBach.Components.Models;
using ZumGelbenBach.Components;

var builder = WebApplication.CreateBuilder(args);

// Füge die Razor-Komponenten hinzu
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<SessionDataService>();
builder.Services.AddSingleton<Authentifications>();
builder.Services.AddHttpClient();
builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();

// Konfiguriere die HTTP-Anforderungs-Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// Mappe Razor-Komponenten und aktiviere den Server-Render-Modus
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
