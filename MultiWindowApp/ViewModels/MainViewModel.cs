using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using MultiWindowApp.Extensions;
using MultiWindowApp.Models.Entities;
using MultiWindowApp.Models.Enums;
using MultiWindowApp.Models.Interfaces;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace MultiWindowApp.ViewModels;

public partial class MainViewModel : ViewModelBase, IRoutableViewModel
{
    public IScreen HostScreen { get; }
    public string UrlPathSegment => "main";
    
    private readonly IAsyncUserRepository _userRepository;
    private readonly INavigationService _navigationService;

    private ObservableCollection<UserViewModel> Users { get; set; }
    
    public List<KeyValuePair<Gender, string>> Genders { get; } =
    [
        new(Models.Enums.Gender.Unspecified, "Не указан"),
        new(Models.Enums.Gender.Male, "Мужской"),
        new(Models.Enums.Gender.Female, "Женский")
    ];
    
    public DateTimeOffset? BirthDatePicker
    {
        get => BirthDate.ToDateTime(TimeOnly.MinValue, DateTimeKind.Unspecified);
        set
        {
            if (value.HasValue)
                BirthDate = DateOnly.FromDateTime(value.Value.DateTime);
        }
    }
    
    [Reactive] private UserViewModel? _selectedUser;
    [Reactive] private UserViewModel? _originalUser;
    [Reactive] private int? _id;
    [Reactive] private string? _firstName;
    [Reactive] private string? _lastName;
    [Reactive] private Gender _gender;
    [Reactive] private DateOnly _birthDate;
    [Reactive] private string? _email;
    
    private readonly bool _isAdmin;
    
    [Reactive] private string _notificationText = string.Empty;
    [Reactive] private bool _isNotificationVisible;
    
    private IDisposable? _notificationTimer;

    private readonly IObservable<bool> _canClear;
    private readonly IObservable<bool> _canSave;
    private readonly IObservable<bool> _canDelete;
    
    public MainViewModel(IAsyncUserRepository userRepository, INavigationService navigationService, IScreen hostScreen, UserEntity userEntity)
    {
        if (userEntity.IsDeleted)
        {
            throw new ArgumentException("User can't be deleted");
        }
        HostScreen = hostScreen;
        _userRepository = userRepository;
        _navigationService = navigationService;
        _isAdmin = userEntity.IsAdmin;
        this.WhenAnyValue(x => x.SelectedUser)
            .Subscribe(su =>
            {
                Id = su?.Id;
                LastName = su?.LastName;
                FirstName = su?.FirstName;
                Gender = su?.Gender ?? Gender.Unspecified;
                Email = su?.Email;
                BirthDate = su?.BirthDate ?? default;
                
                OriginalUser = userEntity.ToUserViewModel();
            });

        Users = _isAdmin
            ? new ObservableCollection<UserViewModel>(_userRepository.GetUsersAsync(null).Result.ToUserViewModels())
            : [];
        Users.Insert(0, new UserViewModel(userEntity));

        _canClear = this.WhenAnyValue(
            x => x.FirstName,
            x => x.LastName,
            x => x.Gender,
            x => x.BirthDate,
            x => x.Email,
            (firstName, lastName, gender, birthDate, email) =>
                !string.IsNullOrWhiteSpace(firstName) ||
                !string.IsNullOrWhiteSpace(lastName) ||
                gender != Gender.Unspecified ||
                birthDate != default ||
                !string.IsNullOrWhiteSpace(email));

        _canSave = this.WhenAnyValue(
            x => x.Id,
            x => x.FirstName,
            x => x.LastName,
            x => x.Gender,
            x => x.BirthDate,
            x => x.Email,
            (id, firstName, lastName, gender, birthDate, email) =>
                id is not null &&
                !string.IsNullOrWhiteSpace(firstName) &&
                !string.IsNullOrWhiteSpace(lastName) &&
                gender != Gender.Unspecified &&
                birthDate != default &&
                !string.IsNullOrWhiteSpace(email));

        _canDelete = this.WhenAnyValue(x => x.SelectedUser)
            .Select(su => su is not null);
        
    }

    private void ShowNotification(string message)
    {
        NotificationText = message;
        
        _notificationTimer?.Dispose();

        IsNotificationVisible = true;

        const int visibilitySeconds = 3;

        _notificationTimer = Observable.Timer(TimeSpan.FromSeconds(visibilitySeconds))
            .Subscribe(_ =>
            {
                IsNotificationVisible = false;
                _notificationTimer?.Dispose();
            });
    }

    [ReactiveCommand(CanExecute = nameof(_canClear))]
    private void Clear()
    {
        Id = null;
        FirstName = null;
        LastName = null;
        Gender = Gender.Unspecified;
        BirthDate = default;
        Email = null;
    }

    [ReactiveCommand(CanExecute = nameof(_canSave))]
    private async Task Save()
    {
        if (SelectedUser is not null)
        {
            var updatedUser = new UserViewModel(SelectedUser);
            var index = Users.IndexOf(SelectedUser);
            Users[index] = updatedUser;
            SelectedUser = updatedUser;
            await _userRepository.UpdateUserAsync(updatedUser.ToUserEntity());
        }
        else
        {
            var newUser = new UserViewModel(
                new UserEntity
                {
                    FirstName = this.FirstName!,
                    LastName = this.LastName!,
                    Gender = this.Gender,
                    BirthDate = this.BirthDate,
                    Email = this.Email!,
                });
            Users.Add(newUser);
            SelectedUser = newUser;
            await _userRepository.UpdateUserAsync(newUser.ToUserEntity());
        }

        ShowNotification("Сохранено!");
        OriginalUser = new UserViewModel(SelectedUser);
    }
    
    [ReactiveCommand(CanExecute = nameof(_canDelete))]
    private async Task Delete()
    {
        Users.Remove(SelectedUser!);
        await _userRepository.SoftDeleteUserAsync(SelectedUser!.Id);
        Clear();
    }
}