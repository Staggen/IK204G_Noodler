using DataLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Repositories {
    public class MatchDataRepository : Repository<MatchDataModels, string> {
        public MatchDataRepository(ApplicationDbContext context) : base(context) { }

        public MatchDataModels GetMatchByUserId(string userId) {
            return items.Find(userId);
        }

        public List<MatchDataModels> GetMatchDataForAllUsersExceptCurrent(string userId) {
            return items.Where((data) => !data.Id.Equals(userId)).ToList();
        }
    }
}
