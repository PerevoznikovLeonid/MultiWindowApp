using Avalonia;
using ReactiveUI.Avalonia;
using System;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using MultiWindowApp.Models.Enums;
using MultiWindowApp.Models.Services;
using MultiWindowApp.ViewModels;
using MultiWindowApp.Views;
using Npgsql;
using ReactiveUI;
using ReactiveUI.Avalonia.Splat;

namespace MultiWindowApp;

sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
#if DEBUG
            .WithDeveloperTools()
#endif
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUIWithMicrosoftDependencyResolver(services =>
            {
                const string connectionString = "Host=localhost;Database=users;Username=postgres;Password=123";
                var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
                dataSourceBuilder.MapEnum<Gender>();
                var dataSource = dataSourceBuilder.Build();
                services.AddSingleton(dataSource);
    
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                
                services.AddScoped<AsyncUserRepository>();
                
                // ViewModel
                services.AddTransient<MainWindowViewModel>();
                services.AddTransient<LoginViewModel>();
                services.AddTransient<MainViewModel>();
                services.AddTransient<RegistrationViewModel>();
                
                // ViewFor
                services.AddTransient<IViewFor<LoginViewModel>, LoginView>();
                services.AddTransient<IViewFor<MainViewModel>, MainView>();
                services.AddTransient<IViewFor<RegistrationViewModel>, RegistrationView>();
            });
}