using System.Text.Json.Serialization;

public class LanguageAsset
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("user_updated")]
    public object UserUpdated { get; set; }

    [JsonPropertyName("date_updated")]
    public object DateUpdated { get; set; }

    [JsonPropertyName("key")]
    public string Key { get; set; }

    [JsonPropertyName("de_DE")]
    public string DeDE { get; set; }

    [JsonPropertyName("en_EN")]
    public string EnEN { get; set; }
}

public class LanguageRoot
{
    [JsonPropertyName("data")]
    public List<LanguageAsset> Data { get; set; }
}