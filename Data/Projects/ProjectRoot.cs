using System.Text.Json.Serialization;

namespace ERmain.Data.Projects;

public class Project
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("listing")]
    public int Listing { get; set; }

    [JsonPropertyName("TitleEN")]
    public string TitleEN { get; set; }

    [JsonPropertyName("TitleDE")]
    public string TitleDE { get; set; }

    [JsonPropertyName("DescriptionEN")]
    public string DescriptionEN { get; set; }

    [JsonPropertyName("DescriptionDE")]
    public string DescriptionDE { get; set; }

    [JsonPropertyName("ERProjectsID")]
    public int ERProjectsID { get; set; }
}

public class ProjectRoot
{
    [JsonPropertyName("data")]
    public List<Project> Data { get; set; }
}

