namespace ThesisOct2023.Models.ViewModels
{
    public class ChartsView
    {
        public IEnumerable<FoodChartViewModel> charts = Enumerable.Empty<FoodChartViewModel>();
        public PagingInfo PagingInfo { get; set; } = new();
    }
}
