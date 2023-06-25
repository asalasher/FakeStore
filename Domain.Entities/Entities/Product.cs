namespace FS.Domain.Entities.Entities
{

    public class Product
    {
        public int? Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Image { get; set; }
        public Rating? Rating { get; set; }
    }

    public class Rating
    {
        public decimal? Rate { get; set; }
        public int? Count { get; set; }
    }

}