using Blazored.Toast.Services;
using System.Net.Http.Json;
using TimeTracker.Shared.Models.Account;

namespace TimeTracker.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private readonly IToastService _toastService;

        public AuthService(HttpClient http, IToastService toastService)
        {
            _http = http;
            _toastService = toastService;
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
                }
            }

        }
    }
}
