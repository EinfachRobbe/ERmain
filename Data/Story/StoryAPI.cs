using System.Globalization;
using System.Net.Http.Json;
using ERmain.Data.Language;
using Microsoft.Extensions.Caching.Memory;

namespace ERmain.Data.Story;

public class StoryAPI
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _memoryCache;
    private readonly LanguageService _languageService;
    
    public StoryAPI(HttpClient httpClient, IMemoryCache memoryCache, LanguageService languageService)
    {
        _httpClient = httpClient;
        _memoryCache = memoryCache;
        _languageService = languageService;
        
        _httpClient.BaseAddress = new Uri("https://cms.einfachrobbe.de/");
    }
    
    public async Task<StoryRoot> GetStoryRoot()
    {
        if (_memoryCache.TryGetValue("StoryRoot", out StoryRoot cachedStoryRoot))
        {
            return cachedStoryRoot;
        }
        
        StoryRoot storyRoot = await _httpClient.GetFromJsonAsync<StoryRoot>($"items/ERmainStory");
        
        foreach (var storyAsset in storyRoot.Data)
        {
            storyAsset.MonthDE = new DateTime(1, int.Parse(storyAsset.MonthEN), 1).ToString("MMMM", new CultureInfo("de-DE"));
            storyAsset.MonthEN = new DateTime(1, int.Parse(storyAsset.MonthEN), 1).ToString("MMMM", new CultureInfo("en-US"));
        }
        
        _memoryCache.Set("StoryRoot", storyRoot, TimeSpan.FromMinutes(5));

        return storyRoot;
    }
    
    public async Task<List<StoryAsset>> GetStoryAssets()
    {
        List<StoryAsset> storyAssets = new();
        StoryRoot storyRoot = await GetStoryRoot();
        storyRoot.Data.ForEach(storyAsset => storyAssets.Add(storyAsset));
        return storyAssets;
    }
    
    public async Task<List<StoryAsset>> GetStoryAssetsByYear(string year)
    {
        List<StoryAsset> storyAssets = new();
        StoryRoot storyRoot = await GetStoryRoot();
        storyRoot.Data.ForEach(storyAsset => {
            if (storyAsset.Year == year)
            {
                storyAssets.Add(storyAsset);
            }
        });
        storyAssets.OrderBy(storyAsset => storyAsset.MonthEN);
        return storyAssets;
    }
    
    public async Task<List<string>> GetStoryYears()
    {
        List<string> storyYears = new();
        StoryRoot storyRoot = await GetStoryRoot();
        
        storyRoot.Data.ForEach(storyAsset => {
            if (!storyYears.Contains(storyAsset.Year))
            {
                storyYears.Add(storyAsset.Year);
            }
        });
        storyYears.Sort();
        return storyYears;
    }
    
}