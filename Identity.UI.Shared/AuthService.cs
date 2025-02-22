using Blazored.LocalStorage;
using Identity.UI.Shared.Auth;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly ApiAuthenticationStateProvider _authenticationStateProvider;

    public AuthService(HttpClient httpClient, ILocalStorageService localStorage, ApiAuthenticationStateProvider authenticationStateProvider)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task Login(string token)
    {
        await _localStorage.SetItemAsync("authToken", token);
        _authenticationStateProvider.MarkUserAsAuthenticated(token);
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("authToken");
        _authenticationStateProvider.MarkUserAsLoggedOut();
    }
}
