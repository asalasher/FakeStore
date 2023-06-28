using System.Text.Json.Serialization;

namespace FS.Domain.Entities.Entities
{

    public class Product
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public decimal? Price { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("category")]
        public string? Category { get; set; }

        [JsonPropertyName("image")]
        public string? Image { get; set; }

        [JsonPropertyName("rating")]
        public Rating? Rating { get; set; }
    }

    public class Rating
    {
        [JsonPropertyName("rate")]
        public decimal? Rate { get; set; }

        [JsonPropertyName("count")]
        public int? Count { get; set; }
    }

}