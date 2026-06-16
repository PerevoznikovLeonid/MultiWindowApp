using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using MultiWindowApp.ViewModels;
using MultiWindowApp.Views;
using Splat;

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
            var mainWindowViewModel = Locator.Current.GetService<MainWindowViewModel>();
            desktop.MainWindow = new MainWindow
            {
                DataContext = mainWindowViewModel
            };
        }
    
        base.OnFrameworkInitializationCompleted();
    }
}