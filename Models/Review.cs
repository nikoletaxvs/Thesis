using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThesisOct2023.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Food")]
        public int FoodId { get; set; }
        [Required]
        public string StudentEmail { get; set; }
        public string? Comments { get; set; }    
        public ICollection<Question> Questions { get; set; }
        public Food Food { get; set; }
    }
}
