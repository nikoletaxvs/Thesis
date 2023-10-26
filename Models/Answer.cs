using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ThesisOct2023.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public string? Comment { get; set; }
        [Required]
        public int StudentId { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; }  
        public Question Question { get; set; } = null!;
    }
}
