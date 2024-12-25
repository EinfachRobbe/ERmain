using System.Net.Http.Json;
using Microsoft.Extensions.Caching.Memory;

namespace ERmain.Data.Language;

public class LanguageAPI
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _memoryCache;
    private readonly LanguageService _languageService;
    
    public LanguageAPI(HttpClient httpClient, IMemoryCache memoryCache, LanguageService languageService)
    {
        _httpClient = httpClient;
        _memoryCache = memoryCache;
        _languageService = languageService;
        
        _httpClient.BaseAddress = new Uri("https://cms.einfachrobbe.de/");
    }
    
    public async Task<LanguageRoot> GetLanguageRoot()
    {
        if (_memoryCache.TryGetValue("LanguageRoot", out LanguageRoot cachedLanguageRoot))
        {
            return cachedLanguageRoot;
        }
        
        LanguageRoot languageRoot = await _httpClient.GetFromJsonAsync<LanguageRoot>($"items/ERmainTranslations");
        
        _memoryCache.Set("LanguageRoot", languageRoot, TimeSpan.FromMinutes(5));

        return languageRoot;
    }
    
    public async Task<List<LanguageAsset>> GetLanguageAssets()
    {
        List<LanguageAsset> languageAssets = new();
        LanguageRoot languageRoot = await GetLanguageRoot();
        languageRoot.Data.ForEach(languageAsset => languageAssets.Add(languageAsset));
        return languageAssets;
    }
    
    public async Task<LanguageAsset> GetLanguageAsset(string key)
    {
        LanguageRoot languageRoot = await GetLanguageRoot();
        return languageRoot.Data.FirstOrDefault(languageAsset => languageAsset.Key == key);
    }
    
}