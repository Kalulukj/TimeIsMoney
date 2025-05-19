using Microsoft.Maui.Controls.Compatibility.Hosting;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;

namespace TimeIsMoney;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseDevExpress()
            .UseLocalNotification()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Nunito-Regular.ttf", "NunitoRegular");
                fonts.AddFont("Nunito-Italic.ttf", "NunitoItalic");
            })
             .UseMauiCompatibility();

#if DEBUG
        builder.Logging.AddDebug();
#endif
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
        {
            h.PlatformView.BackgroundTintList =
                Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
        });
        Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
        {
            h.PlatformView.BackgroundTintList =
                Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
        });
        return builder.Build();
    }
}