using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using MultiWindowApp.Models.Enums;
using MultiWindowApp.Models.Services;
using MultiWindowApp.ViewModels;
using MultiWindowApp.Views;
using Npgsql;
using ReactiveUI;
using Splat.Microsoft.Extensions.DependencyInjection;

namespace MultiWindowApp;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        const string connectionString = "Host=localhost;Database=users;Username=postgres;Password=123";
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        dataSourceBuilder.MapEnum<Gender>();
        var dataSource = dataSourceBuilder.Build();
        
        // TODO: Добавить RegistrationView
        var services = new ServiceCollection();
        
        services.AddSingleton(dataSource);
        
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        
        services.AddScoped<AsyncUserRepository>();
        
        // ViewModels
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<LoginViewModel>();
        services.AddTransient<MainViewModel>();
        
        // ViewFor
        services.AddTransient<IViewFor<LoginViewModel>, LoginView>();
        services.AddTransient<IViewFor<MainViewModel>, MainView>();
        
        var serviceProvider = services.BuildServiceProvider();
        serviceProvider.UseMicrosoftDependencyResolver();
        
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