using MultiWindowApp.Models.Entities;
using ReactiveUI;

namespace MultiWindowApp.Models.Interfaces;

public interface INavigationService
{
    void NavigateToMain(IScreen hostScreen, UserEntity userEntity);
    void NavigateToRegistration(IScreen hostScreen);
    void NavigateToLogin(IScreen hostScreen);
}