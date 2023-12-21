namespace ThesisOct2023.Models.ViewModels
{
    public class FoodReviewView
    {
        public Food food { get; set; }
        public List<Review> reviews { get; set;}
        public List<string> Comments { get; set;}
    }
}
