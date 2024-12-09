using Blazored.LocalStorage;

namespace ERmain.Data.Language;

public class LanguageService
{
    private readonly ILocalStorageService _localStorage;
    private readonly ILogger<LanguageService> _logger;
    
    public event Action? OnLanguageChanged; 
    
    public LanguageService(ILocalStorageService localStorage, ILogger<LanguageService> logger)
    {
        _localStorage = localStorage;
        _logger = logger;
    }
    
    public async Task<string> GetLanguage()
    {
        return await _localStorage.GetItemAsStringAsync("lang");
    }
    
    public async Task SetLanguage(string lang)
    {
        await _localStorage.SetItemAsStringAsync("lang", lang);
    }
    
    public async Task<string> GetLanguageOrDefault()
    {
        string lang = await GetLanguage();
        string newLang = string.IsNullOrWhiteSpace(lang) ? "en" : lang;
        await SetLanguage(newLang);
        return newLang;
    }
    
    public async Task ToggleLanguage()
    {
        _logger.LogInformation("Language change called");
        string lang = await GetLanguageOrDefault();
        string newLang = lang == "en" ? "de" : "en";
        await SetLanguage(newLang);
        NotifyLanguageChanged();
    }
    
    private void NotifyLanguageChanged()
    {
        _logger.LogInformation("Language change invoked");
        OnLanguageChanged?.Invoke();
    }
}