using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;
using Microsoft.Extensions.Logging;

namespace ProjectNasa;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		//var builder = MauiApp.CreateBuilder();
		//builder
		//	.UseMauiApp<App>()
		//	.ConfigureFonts(fonts =>
		//	{
		//		fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
		//		fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
		//	});

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            // Initialize the .NET MAUI Community Toolkit by adding the below line of code
            .UseMauiCommunityToolkit()
            // After initializing the .NET MAUI Community Toolkit, optionally add additional fonts
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<IApodService, ApodService>();
        builder.Services.AddSingleton<ApodViewModel>();
        builder.Services.AddSingleton<ApodPage>();
        builder.Services.AddSingleton(FileSaver.Default);

        builder.Services.AddTransient<ImageViewModel>();
		builder.Services.AddTransient<ImagePage>();

        builder.Services.AddSingleton(Connectivity.Current);

        return builder.Build();
	}
}
