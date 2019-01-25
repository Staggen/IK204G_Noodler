using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Models;

namespace NoodlerMVC.Models {
    public class FriendViewModels {
        public int RelationId { get; set; }
        public ProfileModels Friend { get; set; }
    }
    public class FriendViewModelsForUsers {
        public string UserId { get; set; }
        public List<FriendViewModels> FriendList { get; set; }
    }
}