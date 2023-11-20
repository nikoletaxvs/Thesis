namespace ThesisOct2023.Models
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserRole { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        
    }

   

}
