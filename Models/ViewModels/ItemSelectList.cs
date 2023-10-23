using Microsoft.AspNetCore.Mvc.Rendering;
using System.Transactions;

namespace ThesisOct2023.Models.ViewModels
{
    // Container for items available for selection
    public class ItemSelectList
    {
        //List for the items that are available for selection
        public List<SelectListItem> ItemsSelectList { get; set; }
    }
    //Container for breakfast items available for selection
    public class BreakfastSelectList : ItemSelectList
    {
        public string Category = "Breakfast";
    }
    //Container for launch items available for selection
    public class LaunchSelectList : ItemSelectList
    {
        public string Category = "Launch";
    }
    //Container for dinner items available for selection
    public class DinnerSelectList :ItemSelectList
    {
        public string Category  = "Dinner";
    }
}
