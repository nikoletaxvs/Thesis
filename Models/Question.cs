using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThesisOct2023.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Review")]
        public int ReviewId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        public int Answer { get; set; } //In range 1-5
        public Review Review { get; set; }
    }
}
