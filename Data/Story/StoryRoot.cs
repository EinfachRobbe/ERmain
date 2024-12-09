using System.Text.Json.Serialization;

namespace ERmain.Data.Story;

public class StoryAsset
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("user_updated")]
    public string? UserUpdated { get; set; }

    [JsonPropertyName("date_updated")]
    public DateTime? DateUpdated { get; set; }

    [JsonPropertyName("TitleEN")]
    public string TitleEN { get; set; }

    [JsonPropertyName("TitleDE")]
    public string TitleDE { get; set; }

    [JsonPropertyName("MonthEN")]
    public string MonthEN { get; set; }

    [JsonPropertyName("Year")]
    public string Year { get; set; }

    [JsonPropertyName("DescriptionEN")]
    public string DescriptionEN { get; set; }

    [JsonPropertyName("DescriptionDE")]
    public string DescriptionDE { get; set; }
}

public class StoryRoot
{
    [JsonPropertyName("data")]
    public List<StoryAsset> Data { get; set; }
}

