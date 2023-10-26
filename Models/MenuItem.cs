using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThesisOct2023.Models
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Day { get; set; } //range from 1 to 7
        [Required]
        public string ServedAt { get; set; } //Breakfast,Launch or Dinner

        [ForeignKey("Food")]
        public int FoodId { get; set; }
        public Food Food { get; set; } = null!;

        [ForeignKey("Menu")]
        public int MenuId { get; set; }
        public Menu Menu { get; set; } = null!;
    }
}
