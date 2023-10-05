using System.ComponentModel.DataAnnotations;

namespace ThesisOct2023.Models
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }
      
        public int week { get; set; }
        
        //Many to many relationship with menu items
        public List<MenuItem> MenuItems { get; } = new();
        public List<Food> Foods { get; } = new();

    }
}
