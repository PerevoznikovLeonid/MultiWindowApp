using System;
using Avalonia;
using Avalonia.Controls;
using MultiWindowApp.ViewModels;
using ReactiveUI;

namespace MultiWindowApp.Views;

public partial class LoginView : UserControl, IViewFor<LoginViewModel>
{
    public LoginView()
    {
        InitializeComponent();
    }
    
    public static readonly StyledProperty<LoginViewModel?> ViewModelProperty =
        AvaloniaProperty.Register<LoginView, LoginViewModel?>(nameof(ViewModel));

    public LoginViewModel? ViewModel
    {
        get => GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }
    
    object? IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = (LoginViewModel?)value;
    }
    
    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);
        if (DataContext is LoginViewModel vm)
            ViewModel = vm;
    }
}