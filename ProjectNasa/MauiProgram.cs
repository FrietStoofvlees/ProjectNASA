using Microsoft.Extensions.Logging;

namespace ProjectNasa;

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
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<IApodService, ApodService>();
        builder.Services.AddSingleton<ApodViewModel>();
        builder.Services.AddSingleton<ApodPage>();

        builder.Services.AddSingleton(Connectivity.Current);

        return builder.Build();
	}
}
