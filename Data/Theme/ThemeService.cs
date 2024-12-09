using Blazored.LocalStorage;

namespace ERmain.Data.Theme;

public class ThemeService
{
    private readonly ILocalStorageService _localStorage;
    private readonly ILogger<ThemeService> _logger;
    
    public event Action? OnThemeChanged;
    
    public ThemeService(ILocalStorageService localStorage, ILogger<ThemeService> logger)
    {
        _localStorage = localStorage;
        _logger = logger;
    }
    
    public async Task<string> GetTheme()
    {
        return await _localStorage.GetItemAsStringAsync("theme");
    }
    
    public async Task SetTheme(string theme)
    {
        await _localStorage.SetItemAsStringAsync("theme", theme);
    }
    
    public async Task<string> GetThemeOrDefault()
    {
        string theme = await GetTheme();
        string newTheme = string.IsNullOrWhiteSpace(theme) ? "light" : theme;
        await SetTheme(newTheme);
        return newTheme; 
    }

    public async Task ToggleTheme()
    {
        _logger.LogInformation("Theme change called");
        string theme = await GetThemeOrDefault();
        string newTheme = theme == "light" ? "dark" : "light";
        await SetTheme(newTheme);
        NotifyThemeChanged();
    }

    private void NotifyThemeChanged()
    {
        _logger.LogInformation("Theme change invoked");
        OnThemeChanged?.Invoke();
    }
    
}