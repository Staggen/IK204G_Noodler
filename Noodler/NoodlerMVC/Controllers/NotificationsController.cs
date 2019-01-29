using DataLayer.Models;
using DataLayer.Repositories;
using Microsoft.AspNet.Identity;
using NoodlerMVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NoodlerMVC.Controllers {
    [Authorize]
    public class NotificationsController : Controller
    {
        private RequestRepository requestRepository;
        private ProfileRepository profileRepository;

        public NotificationsController() {
            ApplicationDbContext context = new ApplicationDbContext();
            requestRepository = new RequestRepository(context);
            profileRepository = new ProfileRepository(context);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GetNumberOfNotifications() {
            string currentUserId = User.Identity.GetUserId();
            List<RequestModels> requests = requestRepository.GetAllRequestsSentToUserById(currentUserId);
            return Json(new { Number = requests.Count });
        }

        [HttpGet]
        public PartialViewResult GetFriendRequests() {
            string currentUserId = User.Identity.GetUserId();
            List<RequestModels> requests = requestRepository.GetAllRequestsSentToUserById(currentUserId);
            if (requests.Count >= 1) {
                IEnumerable<RequestViewModels> model = requests.Select((r) => new RequestViewModels() {
                    RequestId = r.Id,
                    UserId = r.RequestFromId,
                    FullName = r.RequestFrom.FirstName + " " + r.RequestFrom.LastName,
                    RequestDateTime = r.RequestDateTime,
                    ProfileImage = profileRepository.Get(r.RequestFromId).ProfileImage
                });
                return PartialView("_Notifications", model);
            } else {
                return PartialView("_NoNotifications");
            }
        }
    }
}