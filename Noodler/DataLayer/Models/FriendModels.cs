using DataLayer.Repositories;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models {
    public class FriendModels : IIdentifiable<int> { // Possibly extend model with category of friend, to add "dating" feature(?)
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public virtual string UserId { get; set; }
        public virtual ProfileModels User { get; set; }

        [ForeignKey("Friend")]
        public virtual string FriendId { get; set; }
        public virtual ProfileModels Friend { get; set; }

        [ForeignKey("Category")]
        public int FriendCategory { get; set; }
        public virtual FriendCategoryModels Category { get; set; }
    }
}
