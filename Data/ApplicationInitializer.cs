using Microsoft.AspNetCore.Identity;
using ThesisOct2023.Models;

namespace ThesisOct2023.Data
{
    public static class ApplicationInitializer
    {
        private static readonly RoleManager<ApplicationUser> _staticRoleManager;
        private static readonly UserManager<ApplicationUser> _staticUserManager;

        public static WebApplication Seed(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                try
                {
                    context.Database.EnsureCreated();
                    /*ADD USER ROLES*/
                    var userRoles = context.Roles.FirstOrDefault();
                    if (userRoles == null) {
                        context.Roles.AddRange(
                           new IdentityRole { Id = "0052a4d2-e241-4f6b-a1ed-fbf332247d54", Name = "Admin", NormalizedName = "ADMIN" },
                           new IdentityRole { Id = "66e2801b-329e-486d-a1b3-f3396815b3b2", Name = "Cook", NormalizedName = "COOK" },
                           new IdentityRole { Id = "9710165b-cecf-4ad5-9efd-ba200abac13e", Name = "Student", NormalizedName = "STUDENT" }
                        );
                    }
                    /*ADD USERS*/
                    //a hasher to hash the password before seeding the user to the db
                    var hasher = new PasswordHasher<ApplicationUser>();
                    var users = context.Users.FirstOrDefault();
                    if (users == null)
                    {
                        List<ApplicationUser> usersList = new List<ApplicationUser>
                        {
                              new ApplicationUser { Id = "1", FirstName = "John", LastName = "Doe", UserRole = "Admin", RegistrationDate = DateTime.UtcNow, PasswordHash = hasher.HashPassword(null, "Student!1") },
                                    new ApplicationUser { Id = "2", FirstName = "Johan", LastName = "Doeh", UserRole = "Cook", RegistrationDate = DateTime.UtcNow, PasswordHash = hasher.HashPassword(null, "Student!1") },
                                    new ApplicationUser { Id = "3", FirstName = "Johan", LastName = "Doea", UserRole = "Student", RegistrationDate = DateTime.UtcNow, PasswordHash = hasher.HashPassword(null, "Student!1") },
                                    new ApplicationUser { Id = "4", FirstName = "Jane", LastName = "Smith", UserRole = "Student", RegistrationDate = DateTime.UtcNow, PasswordHash = hasher.HashPassword(null, "Student!1") },
                                    new ApplicationUser { Id = "5", FirstName = "Michael", LastName = "Johnson", UserRole = "Student", RegistrationDate = DateTime.UtcNow, PasswordHash = hasher.HashPassword(null, "Student!1") },
                                    new ApplicationUser { Id = "6", FirstName = "Emily", LastName = "Brown", UserRole = "Student", RegistrationDate = DateTime.UtcNow, PasswordHash = hasher.HashPassword(null, "Student!1") },
                                    new ApplicationUser { Id = "7", FirstName = "Daniel", LastName = "Davis", UserRole = "Student", RegistrationDate = DateTime.UtcNow, PasswordHash = hasher.HashPassword(null, "Student!1") },
                                    new ApplicationUser { Id = "8", FirstName = "Alex", LastName = "Miller", UserRole = "Student", RegistrationDate = DateTime.UtcNow, PasswordHash = hasher.HashPassword(null, "Student!1") },
                                    new ApplicationUser { Id = "9", FirstName = "Grace", LastName = "Turner", UserRole = "Student", RegistrationDate = DateTime.UtcNow, PasswordHash = hasher.HashPassword(null, "Student!1") },
                                    new ApplicationUser { Id = "10", FirstName = "William", LastName = "Lee", UserRole = "Student", RegistrationDate = DateTime.UtcNow, PasswordHash = hasher.HashPassword(null, "Student!1") },
                                    new ApplicationUser { Id = "11", FirstName = "Olivia", LastName = "Wang", UserRole = "Student", RegistrationDate = DateTime.UtcNow, PasswordHash = hasher.HashPassword(null, "Student!1") },
                                    new ApplicationUser { Id = "12", FirstName = "Henry", LastName = "Gomez", UserRole = "Student", RegistrationDate = DateTime.UtcNow, PasswordHash = hasher.HashPassword(null, "Student!1") },
                                    new ApplicationUser { Id = "13", FirstName = "Zoe", LastName = "Nguyen", UserRole = "Student", RegistrationDate = DateTime.UtcNow, PasswordHash = hasher.HashPassword(null, "Student!1") },
                                    new ApplicationUser { Id = "14", FirstName = "Gabriel", LastName = "Chen", UserRole = "Student", RegistrationDate = DateTime.UtcNow, PasswordHash = hasher.HashPassword(null, "Student!1") },
                                    new ApplicationUser { Id = "15", FirstName = "Aria", LastName = "Patel", UserRole = "Student", RegistrationDate = DateTime.UtcNow, PasswordHash = hasher.HashPassword(null, "Student!1") },
                                    new ApplicationUser { Id = "16", FirstName = "Logan", LastName = "Ramirez", UserRole = "Student", RegistrationDate = DateTime.UtcNow, PasswordHash = hasher.HashPassword(null, "Student!1") },
                                    new ApplicationUser { Id = "17", FirstName = "Lily", LastName = "Reyes", UserRole = "Student", RegistrationDate = DateTime.UtcNow, PasswordHash = hasher.HashPassword(null, "Student!1") },
                                    new ApplicationUser { Id = "18", FirstName = "Ryan", LastName = "Mitchell", UserRole = "Student", RegistrationDate = DateTime.UtcNow, PasswordHash = hasher.HashPassword(null, "Student!1") },
                                    new ApplicationUser { Id = "19", FirstName = "Isabella", LastName = "Turner", UserRole = "Student", RegistrationDate = DateTime.UtcNow, PasswordHash = hasher.HashPassword(null, "Student!1") },
                                    new ApplicationUser { Id = "20", FirstName = "Ethan", LastName = "Lopez", UserRole = "Student", RegistrationDate = DateTime.UtcNow, PasswordHash = hasher.HashPassword(null, "Student!1") },
                                    new ApplicationUser { Id = "21", FirstName = "Sophie", LastName = "Graham", UserRole = "Student", RegistrationDate = DateTime.UtcNow, PasswordHash = hasher.HashPassword(null, "Student!1") },
                                    new ApplicationUser { Id = "22", FirstName = "Caleb", LastName = "Carter", UserRole = "Student", RegistrationDate = DateTime.UtcNow, PasswordHash = hasher.HashPassword(null, "Student!1") },
                                    new ApplicationUser { Id = "23", FirstName = "Madison", LastName = "Cooper", UserRole = "Student", RegistrationDate = DateTime.UtcNow, PasswordHash = hasher.HashPassword(null, "Student!1") },
                                    new ApplicationUser { Id = "24", FirstName = "Nathan", LastName = "Reed", UserRole = "Student", RegistrationDate = DateTime.UtcNow, PasswordHash = hasher.HashPassword(null, "Student!1") },
                                    new ApplicationUser { Id = "25", FirstName = "Ava", LastName = "Fisher", UserRole = "Student", RegistrationDate = DateTime.UtcNow, PasswordHash = hasher.HashPassword(null, "Student!1") },
                                    new ApplicationUser { Id = "26", FirstName = "Lucas", LastName = "Woods", UserRole = "Student", RegistrationDate = DateTime.UtcNow, PasswordHash = hasher.HashPassword(null, "Student!1") },
                                    new ApplicationUser { Id = "27", FirstName = "Chloe", LastName = "Grant", UserRole = "Student", RegistrationDate = DateTime.UtcNow, PasswordHash = hasher.HashPassword(null, "Student!1") }

                        };
                        foreach (ApplicationUser user in usersList)
                        {
                            // Generate email and username based on FirstName and LastName
                            var email = $"{user.FirstName.ToLower()}.{user.LastName.ToLower()}@example.com";
                            var userName = $"{user.FirstName.ToLower()}.{user.LastName.ToLower()}";

                            // Fill the additional fields
                            user.Email = email;
                            user.UserName = userName;
                            user.NormalizedEmail = email.ToUpper();
                            user.NormalizedUserName = userName.ToUpper();
                        }

                        context.Users.AddRange(usersList);
                        context.UserRoles.AddRange(
                            new IdentityUserRole<string> { UserId="1",RoleId= "0052a4d2-e241-4f6b-a1ed-fbf332247d54" },
                            new IdentityUserRole<string> { UserId="2",RoleId= "66e2801b-329e-486d-a1b3-f3396815b3b2" },
                            new IdentityUserRole<string> { UserId="3",RoleId= "9710165b-cecf-4ad5-9efd-ba200abac13e" },
                            new IdentityUserRole<string> { UserId="4",RoleId= "9710165b-cecf-4ad5-9efd-ba200abac13e" },
                            new IdentityUserRole<string> { UserId="5",RoleId= "9710165b-cecf-4ad5-9efd-ba200abac13e" },
                            new IdentityUserRole<string> { UserId="6",RoleId= "9710165b-cecf-4ad5-9efd-ba200abac13e" },
                            new IdentityUserRole<string> { UserId="7",RoleId= "9710165b-cecf-4ad5-9efd-ba200abac13e" },
                            new IdentityUserRole<string> { UserId="8",RoleId= "9710165b-cecf-4ad5-9efd-ba200abac13e" },
                            new IdentityUserRole<string> { UserId="9",RoleId= "9710165b-cecf-4ad5-9efd-ba200abac13e" },
                            new IdentityUserRole<string> { UserId="10",RoleId= "9710165b-cecf-4ad5-9efd-ba200abac13e" },
                            new IdentityUserRole<string> { UserId="11",RoleId= "9710165b-cecf-4ad5-9efd-ba200abac13e" },
                            new IdentityUserRole<string> { UserId="12",RoleId= "9710165b-cecf-4ad5-9efd-ba200abac13e" },
                            new IdentityUserRole<string> { UserId="13",RoleId= "9710165b-cecf-4ad5-9efd-ba200abac13e" },
                            new IdentityUserRole<string> { UserId="14",RoleId= "9710165b-cecf-4ad5-9efd-ba200abac13e" },
                            new IdentityUserRole<string> { UserId="15",RoleId= "9710165b-cecf-4ad5-9efd-ba200abac13e" },
                            new IdentityUserRole<string> { UserId="16",RoleId= "9710165b-cecf-4ad5-9efd-ba200abac13e" },
                            new IdentityUserRole<string> { UserId="17",RoleId= "9710165b-cecf-4ad5-9efd-ba200abac13e" },
                            new IdentityUserRole<string> { UserId="18",RoleId= "9710165b-cecf-4ad5-9efd-ba200abac13e" },
                            new IdentityUserRole<string> { UserId="19",RoleId= "9710165b-cecf-4ad5-9efd-ba200abac13e" },
                            new IdentityUserRole<string> { UserId="20",RoleId= "9710165b-cecf-4ad5-9efd-ba200abac13e" },
                            new IdentityUserRole<string> { UserId="21",RoleId= "9710165b-cecf-4ad5-9efd-ba200abac13e" },
                            new IdentityUserRole<string> { UserId="22",RoleId= "9710165b-cecf-4ad5-9efd-ba200abac13e" },
                            new IdentityUserRole<string> { UserId="23",RoleId= "9710165b-cecf-4ad5-9efd-ba200abac13e" },
                            new IdentityUserRole<string> { UserId="24",RoleId= "9710165b-cecf-4ad5-9efd-ba200abac13e" },
                            new IdentityUserRole<string> { UserId="25",RoleId= "9710165b-cecf-4ad5-9efd-ba200abac13e" },
                            new IdentityUserRole<string> { UserId="26",RoleId= "9710165b-cecf-4ad5-9efd-ba200abac13e" },
                            new IdentityUserRole<string> { UserId="27",RoleId= "9710165b-cecf-4ad5-9efd-ba200abac13e" }
                           
                    );
                    }
                    /*ADD FOOD*/
                    var food = context.Foods.FirstOrDefault();
                    if (food == null)
                    {
                        List<Food> foodList = new List<Food>
                        {
                             new Food {Title="Mousakas",Description= "Moussaka is a layered oven casserole dish made with vegetables and meat. The most well-known version of moussaka is made with layers of eggplant slices, cheese, and a meat sauce, topped with a thick béchamel sauce; however, other favorites call for potatoes, zucchini, or a combination of vegetables.",Category= "Lunch",AvgRating=4,ImageUrl= "8781b892-37ee-4627-ad80-d600e06b1c6b_mousaka.jpg" },
                            new Food {Title= "Pizza",Description= "A flat, open-faced baked pie of Italian origin, consisting of a thin layer of bread dough topped with spiced tomato sauce and cheese, often garnished with anchovies, sausage slices, mushrooms, etc.",Category= "Dinner",AvgRating=5,ImageUrl= "0f3c53c8-c71f-4cbb-b732-e9f129a57e21_pizza.jpg" },
                            new Food {Title="Filter Coffee",Description= "t's a type of coffee made by brewing ground coffee beans using a filter that traps the coffee grounds, allowing the brewed liquid to pass through. The most common filter used is a paper filter, but there are other types like metal filters that also work well.",Category="Breakfast",AvgRating=3,ImageUrl= "a02f1d2d-e513-45ad-a8d2-a26e3702799f_filter coffee.jpg" },
                            new Food { Title= "Salad", Description= "A salad is a dish consisting of mixed ingredients, frequently vegetables. They are typically served chilled or at room temperature, though some can be served warm. Condiments and salad dressings, which exist in a variety of flavors, are often used to enhance a salad.",Category="Dinner",AvgRating=3,ImageUrl= "0c4142e4-d107-4040-a3b4-c1c154cd0e37_salad.jpg" },
                            new Food { Title= "Cereal" ,Description= "Cereal is typically made of grains, such as wheat, oats, rice, or corn. Sugar is added to some cereals to provide a sweet taste. Then, vitamins and flavors are added, and finally other delicious additions like fruits, nuts, and marshmallows.",Category="Breakfast",AvgRating=4,ImageUrl= "631fcb2a-65a5-4e59-ab85-8cb9d2357ccf_cereal.jpg" }

                        };
                        context.Foods.AddRange(foodList);
                    }
                    /*ADD REVIEW QUESTIONS*/
                    var questions = context.Questions.FirstOrDefault();
                    if(questions == null )
                    {
                        List<Question> questions1 = new List<Question>()
                        {
                             new Question {Title= "Appearance",Description= "How would you rate the appearance of the food?" },
                            new Question {Title= "Taste",Description= "How would you rate the taste of the food?" },
                            new Question { Title= "Quality",Description= "How would you rate the quality of the food?" },
                            new Question {Title= "Texture", Description= "How would you rate the Texture of the food?" },
                            new Question { Title= "Ingredients", Description= "Are the ingredients fresh and of good quality????" }
                        };
                        context.Questions.AddRange(questions1);
                    }

                     context.SaveChanges();
                }catch(Exception ex)
                {
                    throw;
                }
                return app;
            }
        }
    }
}
