using System;
using Microsoft.Extensions.DependencyInjection;
using MultiWindowApp.Models.DAOs;
using MultiWindowApp.Models.Interfaces;
using MultiWindowApp.ViewModels;
using ReactiveUI;

namespace MultiWindowApp.Models.Services;

public class NavigationService(IServiceProvider serviceProvider): INavigationService
{
    public void NavigateToMain(IScreen hostScreen, UserDao userDao)
    {
        var vm = ActivatorUtilities.CreateInstance<MainViewModel>(serviceProvider, hostScreen, userDao);
        hostScreen.Router.Navigate.Execute(vm);
    }
    
    public void NavigateToRegistration(IScreen hostScreen)
    {
        var vm = ActivatorUtilities.CreateInstance<RegistrationViewModel>(serviceProvider, hostScreen);
        hostScreen.Router.Navigate.Execute(vm);
    }

    public void NavigateToLogin(IScreen hostScreen)
    {
        var vm = ActivatorUtilities.CreateInstance<LoginViewModel>(serviceProvider, hostScreen);
        hostScreen.Router.Navigate.Execute(vm);
    }
}