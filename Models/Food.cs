using System.ComponentModel.DataAnnotations;

namespace ThesisOct2023.Models
{
    public class Food
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        //public string Day { get; set; } // Number in range 1-7 
        public string? Description { get; set; } 
        public string? Category{ get; set;}
        public double? AvgRating { get; set; }
        //public byte[] Photo { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        //Many to many relationship with Menu
        public List<MenuItem> MenuItems { get; } = new();
        public List<Menu> Menus { get; } = new();
        public List<Review> Reviews { get; } = new();


    }
}
