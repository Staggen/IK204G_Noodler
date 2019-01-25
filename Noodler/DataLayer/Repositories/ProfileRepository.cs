using DataLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Repositories {
    public class ProfileRepository : Repository<ProfileModels, string> {
        public ProfileRepository(ApplicationDbContext context) : base(context) { }

        public List<ProfileModels> GetAllProfilesExceptCurrent(string Id) {
            return items.Where((p) => !p.Id.Equals(Id) && p.IsActive).ToList();
        }

        public List<ProfileModels> GetAllActiveProfiles() {
            return items.Where((p) => p.IsActive).ToList();
        }
    }
}