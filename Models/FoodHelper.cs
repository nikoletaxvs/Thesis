using System.ComponentModel.DataAnnotations;

namespace ThesisOct2023.Models
{
    public class FoodHelper
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public int? AvgRating { get; set; }
        [Range(1,5,ErrorMessage ="Value should be between 1 and 5")]
        public int healthValue { get; set; }
        public IFormFile Photo { get; set; }
        public string? ImageUrl { get; set; }
    }
}
