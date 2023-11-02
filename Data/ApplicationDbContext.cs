using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ThesisOct2023.Models;

namespace ThesisOct2023.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Question> Questions { get; set; }  
        
    }
}