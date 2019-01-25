using DataLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Repositories {
    public class RequestRepository : Repository<RequestModels, int> {
        public RequestRepository(ApplicationDbContext context) : base(context) { }

        public List<RequestModels> GetAllRequestsSentByUserId(string userId) {
            return items.Where((r) => r.RequestFromId.Equals(userId)).ToList();
        }

        public List<RequestModels> GetAllRequestsSentToUserById(string userId) {
            return items.Where((r) => r.RequestToId.Equals(userId)).ToList();
        }

        public bool OutgoingRequestPending(string currentUserId, string userId) {
            List<RequestModels> query = items.Where((x) => x.RequestFromId.Equals(currentUserId) && x.RequestToId.Equals(userId)).ToList();
            if (query.Count >= 1) return true;
            else return false;
        }

        public bool IncomingRequestPending(string currentUserId, string userId) {
            List<RequestModels> query = items.Where((x) => x.RequestToId.Equals(currentUserId) && x.RequestFromId.Equals(userId)).ToList();
            if (query.Count >= 1) return true;
            else return false;
        }

        public bool RequestPending(string userId, string currentUserId) {
            List<RequestModels> query = items.Where((x) => x.RequestFromId.Equals(currentUserId) && x.RequestToId.Equals(userId) || x.RequestFromId.Equals(userId) && x.RequestToId.Equals(currentUserId)).ToList();
            if (query.Count > 0) return true;
            else return false;
        }

        public bool RequestReceived(string userId, string currentUserId) {
            List<RequestModels> query = items.Where((x) => x.RequestFromId.Equals(userId) && x.RequestToId.Equals(currentUserId)).ToList();
            if (query.Count > 0) return true;
            else return false;
        }
    }
}
