using System.Reflection;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using MultiWindowApp.ViewModels;
using MultiWindowApp.Views;
using ReactiveUI;
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
        const string connectionString = "Data Source=users.db";
        //TODO: Добавить RegistrationView
        var services = new ServiceCollection();
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<LoginViewModel>();
        services.AddTransient<MainViewModel>();
        services.AddTransient<IViewFor<LoginViewModel>, LoginView>();
        services.AddTransient<IViewFor<MainViewModel>, MainView>();
        
        switch (ApplicationLifetime)
        {
            case IClassicDesktopStyleApplicationLifetime desktop:
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel()
                };
                break;
            case ISingleViewApplicationLifetime singleView:
                singleView.MainView = new MainView
                {
                    DataContext = new MainWindowViewModel()
                };
                break;
        }
        
        base.OnFrameworkInitializationCompleted();
    }
}