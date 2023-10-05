using Azure;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace ThesisOct2023.Models
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int FoodId { get; set; }
        public Food Food { get; set; } = null!;
        public Menu Menu { get; set; } = null!;
        public int Day { get; set; } //range from 1 to 7
    }
}
