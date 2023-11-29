using System.ComponentModel.DataAnnotations;

namespace ThesisOct2023.Models
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int week { get; set; }// weeks in iso8601 format
        public int avgHealthValue { get; set; }
        public int avgSatisfactionValue { get; set; }
        
        //Many to many relationship with menu items
        public List<MenuItem> MenuItems { get; } = new();
        public List<Food> Foods { get; } = new();

    }
}
