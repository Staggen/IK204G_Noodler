using DataLayer.Models;
using DataLayer.Repositories;
using Microsoft.AspNet.Identity;
using NoodlerMVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NoodlerMVC.Controllers {
    [Authorize]
    public class SearchController : Controller {
        private ProfileRepository profileRepository;

        public SearchController() {
            ApplicationDbContext context = new ApplicationDbContext();
            profileRepository = new ProfileRepository(context);
        }

        //public ActionResult Index() {
        //    List<ProfileModels> allProfiles = profileRepository.GetAllProfilesExceptCurrent(User.Identity.GetUserId());

        //    return View(allProfiles);
        //}

        public ActionResult Index() {
            string currentUser = User.Identity.GetUserId();
            List<ProfileModels> allProfiles = profileRepository.GetAllProfilesExceptCurrent(currentUser);
            ProfileModels currentUserProfile = profileRepository.Get(currentUser);
            List<SearchViewModels> convertedList = new List<SearchViewModels>();

            foreach (ProfileModels item in allProfiles) { // Convert from ProfileModels to SearchViewModels and add to list
                convertedList.Add(MatchUserAgainstOtherUser(currentUserProfile, item));
            }
            return View(convertedList.OrderByDescending((p) => p.Percentage)); // Sort list to have users appear in the search window by match percentage by default
        }

        [HttpPost]
        public PartialViewResult GetMatchPartial(string profileId) {
            ProfileModels currentUserProfile = profileRepository.Get(User.Identity.GetUserId());
            ProfileModels visitedProfile = profileRepository.Get(profileId);

            // Return (PartialView, model);
            return PartialView("_MatchPop", MatchUserAgainstOtherUser(currentUserProfile, visitedProfile));
        }

        private SearchViewModels MatchUserAgainstOtherUser(ProfileModels currentUserData, ProfileModels profileData) {
            int currentUserDesperation = currentUserData.Desperation;
            int currentUserLoneliness = currentUserData.Loneliness;
            int currentUserHorniness = currentUserData.Horniness;
            int currentUserPride = currentUserData.Pride;

            int profileDesperation = profileData.Desperation;
            int profileLoneliness = profileData.Loneliness;
            int profileHorniness = profileData.Horniness;
            int profilePride = profileData.Pride;

            decimal matchDesperationPercentage = 0;
            decimal matchLonelinessPercentage = 0;
            decimal matchHorninessPercentage = 0;
            decimal matchPridePercentage = 0;

            // Check all values against each other individually before later adding them all together for the final percentage.

            if (currentUserDesperation > profileDesperation) {
                matchDesperationPercentage = (decimal)profileDesperation / currentUserDesperation;
            } else {
                matchDesperationPercentage = (decimal)currentUserDesperation / profileDesperation;
            }

            if (currentUserLoneliness > profileLoneliness) {
                matchLonelinessPercentage = (decimal)profileLoneliness / currentUserLoneliness;
            } else {
                matchLonelinessPercentage = (decimal)currentUserLoneliness / profileLoneliness;
            }

            if (currentUserHorniness > profileHorniness) {
                matchHorninessPercentage = (decimal)profileHorniness / currentUserHorniness;
            } else {
                matchHorninessPercentage = (decimal)currentUserHorniness / profileHorniness;
            }

            if (currentUserPride > profilePride) {
                matchPridePercentage = (decimal)profilePride / currentUserPride;
            } else {
                matchPridePercentage = (decimal)currentUserPride / profilePride;
            }

            // Add the individual checks together and divide by the number of individual checks, multiplied by 100 to get a percentage value.

            decimal matchResult = ((matchDesperationPercentage + matchLonelinessPercentage + matchHorninessPercentage + matchPridePercentage) / 4) * 100;

            return new SearchViewModels { Profile = profileData, Percentage = (int)matchResult };
        }
    }
}