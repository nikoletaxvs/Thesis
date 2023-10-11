namespace ThesisOct2023.Models
{
    public class FoodHelper
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public int? AvgRating { get; set; }
        public IFormFile Photo { get; set; }
        public string? ImageUrl { get; set; }
    }
}
