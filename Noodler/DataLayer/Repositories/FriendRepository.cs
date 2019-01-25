using DataLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Repositories {
    public class FriendRepository : Repository<FriendModels, int> {
        public FriendRepository(ApplicationDbContext context) : base(context) { }

        public List<FriendModels> GetAllFriendsByUserId(string userId) {
            return items.Where((f) => f.UserId.Equals(userId) || f.FriendId.Equals(userId) && f.Friend.IsActive && f.User.IsActive).ToList();
        }

        public bool IsFriendAlready(string userId, string currentUserId) {
            List<FriendModels> query = items.Where((x) => x.UserId.Equals(currentUserId) && x.FriendId.Equals(userId) || x.FriendId.Equals(currentUserId) && x.UserId.Equals(userId)).ToList();
            if (query.Count > 0) return true;
            else return false;
        }
    }
}
