using DataLayer.Models;

namespace NoodlerMVC.Models {
    public class SearchViewModels {
        public int Percentage { get; set; }
        public virtual ProfileModels Profile { get; set; }
    }
}