using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using TimeTracker.Shared.Models.Account;
using TimeTracker.Shared.Models.Login;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TimeTracker.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private readonly IToastService _toastService;
        private readonly NavigationManager _navigationManager;
        private readonly ILocalStorageService _localStorageService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthService(HttpClient http, IToastService toastService, NavigationManager navigationManager, 
            ILocalStorageService localStorageService, AuthenticationStateProvider authStateProvider)
        {
            _http = http;
            _toastService = toastService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
            _authenticationStateProvider = authStateProvider;
        }

        public async Task Login(LoginRequest request)
        {
            var result = await _http.PostAsJsonAsync("api/login", request);
            if (result != null)
            {
                var response = result.Content.ReadFromJsonAsync<LoginResponse>().Result;
                if (!response.IsSuccessful && response.Error != null)
                {
                    _toastService.ShowError(response.Error);
                }
                else if (!response.IsSuccessful)
                {
                    _toastService.ShowError("An unexpected error occurred.");
                }
                else
                {
                    if (response.Token != null)
                    {
                        await _localStorageService.SetItemAsync("authToken", response.Token);
                        await _authenticationStateProvider.GetAuthenticationStateAsync();
                        //_http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", response.Token);
                    }
                    _navigationManager.NavigateTo("timeentries");
                    
                }
            }
        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync("authToken");
            await _authenticationStateProvider.GetAuthenticationStateAsync();
            _navigationManager.NavigateTo("login");
        }

        public async Task Register(AccountRegistrationRequest request)
        {
            var result = await _http.PostAsJsonAsync("api/account", request);
            if (result != null)
            {
                var response = await result.Content.ReadFromJsonAsync<AccountRegistrationResponse>();
                if (!response.IsSuccessful && response.Errors != null)
                {
                    foreach (var error in response.Errors)
                    {
                        _toastService.ShowError(error);
                    }
                }
                else if (!response.IsSuccessful)
                {
                    _toastService.ShowError("An unexpected error occurred");
                }
                else
                {
                    _toastService.ShowSuccess("Registration successful! You may login now!");
                    _navigationManager.NavigateTo("timeentries");
                }
            }

        }
    }
}
