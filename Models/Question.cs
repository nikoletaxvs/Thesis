using System.ComponentModel.DataAnnotations;

namespace ThesisOct2023.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        public List<Answer> Answers { get; } = new();
    }
}
