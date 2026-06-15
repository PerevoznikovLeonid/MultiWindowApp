using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MultiWindowApp.ViewModels;
using ReactiveUI;

namespace MultiWindowApp.Views;

public partial class LoginView : UserControl, IViewFor<LoginViewModel>
{
    public LoginView()
    {
        InitializeComponent();
        this.WhenActivated(disposables => { });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    public LoginViewModel? ViewModel
    {
        get => (LoginViewModel?)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    object? IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = (LoginViewModel?)value;
    }

    public static readonly StyledProperty<LoginViewModel?> ViewModelProperty =
        AvaloniaProperty.Register<LoginView, LoginViewModel?>(nameof(ViewModel));
}