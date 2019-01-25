using DataLayer.Repositories;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace NoodlerMVC.Controllers {
    [Authorize]
    public class SearchController : Controller {
        private ProfileRepository profileRepository;

        public SearchController() {
            ApplicationDbContext context = new ApplicationDbContext();
            profileRepository = new ProfileRepository(context);
        }

        // GET: Search
        public ActionResult Index(string searchString) {
            var currentUser = User.Identity.GetUserId();
            var allProfiles = profileRepository.GetAllProfilesExceptCurrent(currentUser);

            return View(allProfiles);
        }
    }
}