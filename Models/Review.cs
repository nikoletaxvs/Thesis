﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThesisOct2023.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string StudentId { get; set; }
        //public string? Comments { get; set; }
        [ForeignKey("Food")]
        public int FoodId { get; set; }
        public Food Food { get; set; }
        public int? SumScore { get; set; } = null;

        public List<ReviewQuestion> ReviewQuestions { get; } = new();
    }
}
