using System.Net.Http.Json;
using ERmain.Data.Language;
using Microsoft.Extensions.Caching.Memory;

namespace ERmain.Data.Projects;

public class ProjectsAPI
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _memoryCache;
    private readonly LanguageService _languageService;
    
    public ProjectsAPI(HttpClient httpClient, IMemoryCache memoryCache, LanguageService languageService)
    {
        _httpClient = httpClient;
        _memoryCache = memoryCache;
        _languageService = languageService;
        
        _httpClient.BaseAddress = new Uri("https://cms.einfachrobbe.de/");
    }
    
    public async Task<ProjectRoot> GetProjectRoot()
    {
        if (_memoryCache.TryGetValue("ProjectRoot", out ProjectRoot cachedProjectRoot))
        {
            return cachedProjectRoot;
        }
        
        ProjectRoot projectRoot = await _httpClient.GetFromJsonAsync<ProjectRoot>($"items/ERmainProjects");
        
        _memoryCache.Set("ProjectRoot", projectRoot, TimeSpan.FromMinutes(5));

        return projectRoot;
    }
    
    public async Task<List<Project>> GetProjects()
    {
        List<Project> projects = new();
        ProjectRoot projectRoot = await GetProjectRoot();
        projectRoot.Data.ForEach(projectAsset => projects.Add(projectAsset));
        projects.OrderBy(project => project.ERProjectsID);
        return projects;
    }
    
}