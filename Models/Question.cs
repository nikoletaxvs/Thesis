using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public List<ReviewQuestion> ReviewQuestions { get; } = new();
    }
}
