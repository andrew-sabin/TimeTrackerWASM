using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace TimeTracker.Client
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;

        public AuthStateProvider(HttpClient http, ILocalStorageService localStorage)
        {
            _http = http;
            _localStorage = localStorage;
        }

        // This class is responsible for providing the authentication state of the user.
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var authToken = await _localStorage.GetItemAsync<string>("authToken");
            AuthenticationState authState;

            // Anonymous user case or the user is not authenticated
            if (string.IsNullOrWhiteSpace(authToken))
            {
                _http.DefaultRequestHeaders.Authorization = null;
                authState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            // Authenticated user case
            else
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                authState = new AuthenticationState(
                    new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(authToken), "jwt")));
            }

            NotifyAuthenticationStateChanged(Task.FromResult(authState));
            return authState;
        }

        // This method is used to parse Json Base64 without padding.
        // The method was created by Steve Sanderson and is used to decode the JWT token payload.
        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
        // This method is used to parse claims from the JWT token.
        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            //var claims = keyValuePairs!.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));

            var claims = new List<Claim>();
            foreach(var kvp in keyValuePairs!)
            {
                if (kvp.Value is JsonElement element && element.ValueKind == JsonValueKind.Array)
                {
                    foreach (var item in element.EnumerateArray())
                    {
                        claims.Add(new Claim(kvp.Key, item.ToString()));
                    }
                }
                else
                {
                    claims.Add(new Claim(kvp.Key, kvp.Value.ToString()!));
                }
            }

            return claims;
        }
    }
}