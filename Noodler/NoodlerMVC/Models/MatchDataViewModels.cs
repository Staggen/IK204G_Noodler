using DataLayer.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NoodlerMVC.Models {
    public class MatchDataViewModels {
        public string ProfileId { get; set; }
        public MatchDataModels ProfileMatchData { get; set; }

        [Display(Name = "Your match percentage against ")]
        public int Percentage { get; set; }
    }

    public class MatchDataViewModelsForUsers {
        public string CurrentUserId { get; set; }
        public List<MatchDataViewModels> UserMatches { get; set; }
    }
}