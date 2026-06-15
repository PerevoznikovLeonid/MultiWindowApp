using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using MultiWindowApp.ViewModels;
using MultiWindowApp.Views;

namespace MultiWindowApp;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new AuthWindow
            {
                DataContext = new RegistrationViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}