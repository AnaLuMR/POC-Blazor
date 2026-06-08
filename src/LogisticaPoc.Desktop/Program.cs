using LogisticaPoc.Client.Services;
using LogisticaPoc.Desktop;
using Microsoft.Extensions.DependencyInjection;
using Photino.Blazor;

namespace LogisticaPoc.Desktop;

internal static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        var appBuilder = PhotinoBlazorAppBuilder.CreateDefault(args);

        appBuilder.Services.AddHttpClient("LogisticaApi", client =>
            client.BaseAddress = new Uri("http://localhost:5150/"));

        appBuilder.Services.AddScoped<ILogisticaService, LogisticaService>();

        appBuilder.RootComponents.Add<App>("#app");

        var app = appBuilder.Build();

        app.MainWindow
            .SetTitle("LogisticaPOC — Desktop")
            .SetSize(1400, 900)
            .Center()
            .SetDevToolsEnabled(false);

        // Evita que o Photino trate a URL como caminho de arquivo (Load("/")).
        app.MainWindow.StartUrl = PhotinoWebViewManager.AppBaseUri.AbsoluteUri;

        AppDomain.CurrentDomain.UnhandledException += (_, e) =>
            app.MainWindow.ShowMessage("Erro Fatal", e.ExceptionObject.ToString() ?? "Erro desconhecido");

        app.Run();
    }
}
