using Microsoft.Maui.Platform;

namespace ProjectNASA;

public partial class App : Application
{
    public static User User;

	public App()
	{
		InitializeComponent();

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
