using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MultiWindowApp.ViewModels;
using ReactiveUI;
using ReactiveUI.Avalonia;

namespace MultiWindowApp.Views;

public partial class RegistrationView : ReactiveUserControl<RegistrationViewModel>
{
    public RegistrationView()
    {
        InitializeComponent();
    }
}