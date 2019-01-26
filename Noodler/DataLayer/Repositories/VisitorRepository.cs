using DataLayer.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataLayer.Repositories {
    public class VisitorRepository : Repository<VisitorModels, int> {
        public VisitorRepository(ApplicationDbContext context) : base(context) { }

        public int GetVisitIdByVisitFromUserId(string userId, string currentUserId) {
            return items.First((v) => v.VisitFromId.Equals(currentUserId) && v.VisitToId.Equals(userId)).Id;
        }

        public List<VisitorModels> GetAllVisitorsByUserId(string userId) {
            return items.Where((v) => v.VisitToId.Equals(userId)).ToList();
        }

        public List<VisitorModels> GetLast5VisitorsByUserId(string userId) {
            List<VisitorModels> visitors = items.Where((v) => v.VisitToId.Equals(userId)).OrderByDescending((x) => x.VisitDateTime).ToList();
            return visitors.GroupBy((x) => x.VisitFromId).Select((y) => y.First()).Take(5).ToList();
        }
    }
}
