using System.Collections.Generic;

namespace Common.ViewModel
{
    public class GetPageResponse<TViewModel> where TViewModel : class
    {
        public int Count { get; set; }
        public List<TViewModel> List { get; set; }
        public bool IsLastPage { get; set; }
        public int CurrentPage { get; set; }
    }
}