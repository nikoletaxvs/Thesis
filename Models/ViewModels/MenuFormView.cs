using Microsoft.AspNetCore.Mvc.Rendering;

namespace ThesisOct2023.Models.ViewModels
{
    public class MenuFormView
    {
        //List for the items that will be selected
        public List<MenuFormViewItem> SelectedItems { get; set; }
        
        //List for the items that are available for selection
        public List<SelectListItem> ItemsSelectList { get; set; }
    }
    public class MenuFormViewItem
    {
        public int Day { get; set; }
        public string BreakFast { get; set; }
        public string Launch { get; set; }
        public string Dinner { get; set; }
    }
}
