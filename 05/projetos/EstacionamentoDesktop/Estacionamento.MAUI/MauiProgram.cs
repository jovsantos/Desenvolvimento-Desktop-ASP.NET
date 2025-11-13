using Microsoft.Extensions.Logging;

namespace Estacionamento.MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif
            string baseAddress = "https://localhost:4000";

#if ANDROID
            baseAddress = "https://10.0.2.2:4000";
#endif

            builder.Services.AddSingleton(sp => new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            });

            return builder.Build();
        }
    }
}
