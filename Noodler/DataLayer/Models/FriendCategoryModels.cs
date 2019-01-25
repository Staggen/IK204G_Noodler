using DataLayer.Repositories;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models {
    public class FriendCategoryModels : IIdentifiable<int>{
        [Key]
        public int Id { get; set; }

        public string CategoryName { get; set; }
    }
}
