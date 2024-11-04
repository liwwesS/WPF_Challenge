using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using WPF_Challenge.DTOs;
using WPF_Challenge.MVVM.Models;

namespace WPF_Challenge.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private UserModel _currentUser;
    private const string MainUrl = "https://petstore.swagger.io/v2";

    public bool IsUserAuthenticated { get; set; }
    public string CurrentUsername { get; set; }

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> LoginAsync(string username, string password)
    {
        var url = $"{MainUrl}/user/login?username={username}&password={password}";

        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            IsUserAuthenticated = true;
            CurrentUsername = username;
        }

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> RegisterAsync(UserDto userDto)
    {
        const string url = $"{MainUrl}/user";

        var response = await _httpClient.PostAsJsonAsync(url, userDto);

        if (response.IsSuccessStatusCode)
        {
            IsUserAuthenticated = true;
            CurrentUsername = userDto.Username;
        }

        return response.IsSuccessStatusCode;
    }

    public async Task LogoutAsync()
    {
        const string url = $"{MainUrl}/user/logout";

        var response = await _httpClient.GetAsync(url);

        IsUserAuthenticated = false;

        CurrentUsername = string.Empty;
    }

    public async Task<UserModel> GetCurrentUser()
    {
        if (string.IsNullOrEmpty(CurrentUsername))
        {
            _currentUser = null;
            return null;
        }

        var url = $"{MainUrl}/user/{CurrentUsername}";

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return null;

        var jsonResponse = await response.Content.ReadAsStringAsync();
        _currentUser = JsonConvert.DeserializeObject<UserModel>(jsonResponse);

        return _currentUser;
    }
}
