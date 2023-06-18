using Microsoft.Maui.Platform;
using System.Text.Json;

namespace ProjectNASA;

public partial class App : Application
{
    public App()
	{
		InitializeComponent();

        Task.Run(async () =>
        {
            string user = await SecureStorage.GetAsync(nameof(AppHelpers.User));
            if (user != null)
            {
                AppHelpers.User = JsonSerializer.Deserialize<User>(user);
            }
        });

        MainPage = new AppShell();

        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("CursorColor", (handler, view) =>
        {
#if ANDROID29_0_OR_GREATER
            if (Current.RequestedTheme == AppTheme.Dark)
            {
                    handler.PlatformView.TextCursorDrawable.SetTint(Colors.White.ToPlatform());
            }
#endif
        });
    }
}
