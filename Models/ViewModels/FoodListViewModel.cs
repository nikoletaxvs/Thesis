namespace ThesisOct2023.Models.ViewModels
{
    public class FoodListViewModel
    {
        public IEnumerable<Food> Foods { get;set; }=Enumerable.Empty<Food>();
        public PagingInfo PagingInfo { get; set; } = new();
        public string? CurrentCategory { get; set; }
    }
}
