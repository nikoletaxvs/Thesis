using System.Collections.Generic;

namespace ThesisOct2023.Models.ViewModels
{
    public class MenuViewModel
    {
        public int weekId { get; set; }
        public List<Food> Monday { get; set; } 
        public List<Food> Tuesday { get; set; }
        public List<Food> Wednesday { get; set; }
        public List<Food> Thursday { get; set; } 
        public List<Food> Friday { get; set; } 
        public List<Food> Saturady { get; set; } 
        public List<Food> Sunday { get; set; } 
        public PagingInfo PagingInfo { get; set; } = new();
       
    }
}
