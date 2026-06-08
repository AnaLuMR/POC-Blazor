using LogisticaPoc.Client.Services;
using LogisticaPoc.Web.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient("LogisticaApi", client =>
    client.BaseAddress = new Uri(
        builder.Configuration["ApiBaseUrl"] ?? "http://localhost:5150/"));

builder.Services.AddScoped<ILogisticaService, LogisticaService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(LogisticaService).Assembly);

app.Run();
