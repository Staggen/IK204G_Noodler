using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DataLayer.Models;
using DataLayer.Repositories;
using Microsoft.AspNet.Identity;

namespace NoodlerMVC.Controllers {
    public class HomeController : Controller {
        private ProfileRepository profileRepository;
        private Random random;

        public HomeController() {
            ApplicationDbContext context = new ApplicationDbContext();
            profileRepository = new ProfileRepository(context);
            random = new Random();
        }
        public ActionResult Index() {
            List<ProfileModels> profiles = new List<ProfileModels>();
            if (User.Identity.IsAuthenticated) {
                profiles = profileRepository.GetAllProfilesExceptCurrent(User.Identity.GetUserId());
            } else {
                profiles = profileRepository.GetAllActiveProfiles();
            }

            List<ProfileModels> randomProfiles = new List<ProfileModels>();
            for(int i=0; i<3; i++) {
                var profile = profiles[random.Next(profiles.Count)];
                if (!randomProfiles.Exists((x) => x == profile)) {
                    randomProfiles.Add(profile);
                } else {
                    i--;
                }
            }

            return View(randomProfiles);
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}