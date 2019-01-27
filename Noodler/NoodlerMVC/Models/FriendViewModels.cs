using DataLayer.Models;
using System.Collections.Generic;

namespace NoodlerMVC.Models {
    public class FriendViewModels {
        public int FriendshipRelationId { get; set; }
        public ProfileModels Friend { get; set; }
        public FriendCategoryModels FriendCategory { get; set; }
    }
    public class FriendViewModelsForUsers {
        public string UserId { get; set; }
        public List<FriendViewModels> FriendList { get; set; }
    }
    public class FriendCategoryViewModels {
        public string ActiveCategory { get; set; }
        public List<FriendCategoryModels> FriendCategories { get; set; }
    }
}