using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThesisOct2023.Models
{
    public class ReviewQuestion
    {
        [Key]
        public int Id { get; set; }
        
        public int? Answer { get; set; }


        [ForeignKey("Review")]
        public int ReviewId { get; set; }
        public Review Review { get; set; } = null!;

        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;

    }
}
