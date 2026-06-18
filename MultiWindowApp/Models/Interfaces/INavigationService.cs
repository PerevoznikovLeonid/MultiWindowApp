using MultiWindowApp.Models.DAOs;
using ReactiveUI;

namespace MultiWindowApp.Models.Interfaces;

public interface INavigationService
{
    void NavigateToMain(IScreen hostScreen, UserDao userDao);
    void NavigateToRegistration(IScreen hostScreen);
    void NavigateToLogin(IScreen hostScreen);
}