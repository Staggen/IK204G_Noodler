using DataLayer.Models;
using DataLayer.Repositories;
using Microsoft.AspNet.Identity;
using NoodlerMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace NoodlerMVC.Controllers {
    [Authorize]
    public class ProfileController : Controller {
        private ProfileRepository profileRepository;
        private PostRepository postRepository;
        private FriendRepository friendRepository;
        private RequestRepository requestRepository;
        private VisitorRepository visitorRepository;

        public ProfileController() {
            ApplicationDbContext context = new ApplicationDbContext();
            profileRepository = new ProfileRepository(context);
            postRepository = new PostRepository(context);
            friendRepository = new FriendRepository(context);
            visitorRepository = new VisitorRepository(context);
            requestRepository = new RequestRepository(context);
        }

        // GET: Profile
        public ActionResult Index() {
            object profileId = Request.RequestContext.RouteData.Values["id"];
            string currentUserId = User.Identity.GetUserId();
            if (profileId == null) {
                profileId = currentUserId;
            }

            // Add user to visitor list for the visited profile, but remove them first if they're already in the 5 latest
            if (!string.Equals(currentUserId, (string)profileId)) {
                List<VisitorModels> allVisitors = visitorRepository.GetAllVisitorsByUserId((string)profileId); // Everyone who has looked at this profile
                if (allVisitors.Any((v) => v.VisitFromId.Equals(currentUserId))) { // If current user has already visited this profile
                    visitorRepository.Remove(visitorRepository.GetVisitIdByVisitFromUserId((string)profileId, currentUserId)); // Remove the visit by the current user
                }
                VisitorModels visitor = new VisitorModels {
                    VisitDateTime = DateTime.Now,
                    VisitFromId = currentUserId,
                    VisitToId = (string)profileId
                };
                visitorRepository.Add(visitor);
                visitorRepository.Save();
            }

            ProfileModels userProfile = profileRepository.Get((string)profileId);
            List<PostModels> posts = postRepository.GetAllPostsForUserById((string)profileId);
            List<FriendModels> friends = friendRepository.GetAllFriendsByUserId((string)profileId);
            PostViewModelsForUsers postViewModelForUsers = ConvertPostToPostViewModelForUsers(posts, (string)profileId);
            FriendViewModelsForUsers friendViewModelsForUsers = ConvertFriendToFriendViewModelsForUsers(friends, (string)profileId);

            ProfileViewModel userProfileViewModel = new ProfileViewModel {
                Id = userProfile.Id,
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                Gender = userProfile.Gender,
                Biography = userProfile.Biography,
                BirthDate = userProfile.BirthDate.ToShortDateString(),
                IsActive = userProfile.IsActive,
                Posts = postViewModelForUsers,
                Friends = friendViewModelsForUsers,
                ProfileImage = userProfile.ProfileImage
            };

            ViewBag.ProfileRelation = GetProfileRelation((string)profileId);

            return View(userProfileViewModel);
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "ProfileImage")] ProfileModels profile) {
            // Excludes ProfileImage from controller call so it doesn't crash.
            if (ModelState.IsValid) {
                // Convert the uploaded photo to a byte array that we can store in the database.
                byte[] imageData = null;
                if (Request.Files.Count == 1) {
                    HttpPostedFileBase poImgFile = Request.Files["ProfileImage"];

                    using (var binary = new BinaryReader(poImgFile.InputStream)) {
                        //This is the byte-array we set as the ProfileImage property on the profile.
                        imageData = binary.ReadBytes(poImgFile.ContentLength);
                    }
                }

                string userId = User.Identity.GetUserId();
                profile.Id = userId;
                profile.ProfileImage = imageData;
                profileRepository.Add(profile);
                profileRepository.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Create");
        }

        public ActionResult Manage() {
            var userId = User.Identity.GetUserId();
            var userProfile = profileRepository.Get(userId);
            return View(userProfile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage([Bind(Exclude = "ProfileImage")] ProfileModels profile) {
            // Excludes ProfileImage from controller call so it doesn't crash.
            if (ModelState.IsValid) {
                // Backup the profile image before possible change
                string profileId = profile.Id;
                byte[] backupImageCopy = profileRepository.Get(profileId).ProfileImage;

                //Possible new profile image input
                byte[] imageData = null;
                if (Request.Files.Count == 1) {
                    HttpPostedFileBase poImgFile = Request.Files["ProfileImage"];

                    using (var binary = new BinaryReader(poImgFile.InputStream)) {
                        //This is the byte-array we set as the ProfileImage property on the profile.
                        imageData = binary.ReadBytes(poImgFile.ContentLength);
                    }
                }

                if (imageData != null && imageData.Length > 0) { // If there is a new file input
                    profile.ProfileImage = imageData;
                } else { // If there is not a new file input
                    profile.ProfileImage = backupImageCopy; // To make sure "null" is not submitted into the database
                }

                profileRepository.Edit(profile);
                profileRepository.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Manage");
        }

        public ActionResult SuspendAccount() {
            ProfileModels profile = profileRepository.Get(User.Identity.GetUserId());
            profile.IsActive = false;
            profileRepository.Edit(profile);
            profileRepository.Save();

            Microsoft.Owin.Security.IAuthenticationManager AuthenticationManager = HttpContext.GetOwinContext().Authentication;
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public PartialViewResult UpdatePostWall(string Id) {
            List<PostModels> posts = postRepository.GetAllPostsForUserById(Id);
            PostViewModelsForUsers model = ConvertPostToPostViewModelForUsers(posts, Id);
            return PartialView("_PostWall", model);
        }

        public PartialViewResult UpdateFriendList(string Id) {
            List<FriendModels> friends = friendRepository.GetAllFriendsByUserId(Id);
            FriendViewModelsForUsers model = ConvertFriendToFriendViewModelsForUsers(friends, Id);
            return PartialView("_Friends", model);
        }

        // Methods that do not return views or partial views below this point

        [AllowAnonymous]
        public FileContentResult RenderProfileImage(string userId) {
            //Converts the stored byte-array to an image. This action is called with razor in views to be used in img tags.
            var profileId = Request.RequestContext.RouteData.Values["id"];
            ProfileModels profile = null;
            if (userId != null) {
                profile = profileRepository.Get(userId);
            } else {
                profile = profileRepository.Get((string)profileId);
            }

            return new FileContentResult(profile.ProfileImage, "image/jpeg");
        }

        public PostViewModelsForUsers ConvertPostToPostViewModelForUsers(List<PostModels> posts, string userId) {
            //Creates an object that contains a List<Post> and properties about the relationship between the inlogged user and the userprofile
            IEnumerable<PostViewModels> postsViewModel = posts.Select((p) => new PostViewModels() {
                PostId = p.Id,
                PostFrom = profileRepository.Get(p.PostFromId),
                Text = p.Text,
                TimeAgo = p.PostDateTime
            });

            PostViewModelsForUsers model = new PostViewModelsForUsers {
                UserId = userId,
                PostList = postsViewModel.ToList()
            };
            return model;
        }

        public FriendViewModelsForUsers ConvertFriendToFriendViewModelsForUsers(List<FriendModels> friends, string userId) {
            List<FriendViewModels> list = new List<FriendViewModels>();
            foreach (FriendModels friend in friends) {
                if (!friend.UserId.Equals(userId)) {
                    list.Add(new FriendViewModels {
                        FriendshipRelationId = friend.Id,
                        Friend = profileRepository.Get(friend.UserId),
                        FriendCategory = friend.Category
                    });
                } else {
                    list.Add(new FriendViewModels {
                        FriendshipRelationId = friend.Id,
                        Friend = profileRepository.Get(friend.FriendId),
                        FriendCategory = friend.Category
                    });
                }
            }
            FriendViewModelsForUsers model = new FriendViewModelsForUsers {
                UserId = userId,
                FriendList = list
            };
            return model;
        }

        public string GetProfileRelation(string ProfileId) {
            string currentUserId = User.Identity.GetUserId();
            if (friendRepository.IsFriendAlready(currentUserId, ProfileId)) {
                return "Friends";
            } else if (requestRepository.IncomingRequestPending(currentUserId, ProfileId)) {
                return "IncomingRequest";
            } else if (requestRepository.OutgoingRequestPending(currentUserId, ProfileId)) {
                return "OutgoingRequest";
            } else {
                return "NotFriends";
            }
        }

        public void ExportProfile() {
            string currentUserId = User.Identity.GetUserId();

            ProfileModels userProfile = profileRepository.Get(currentUserId);

            List<PostModels> posts = postRepository.GetAllPostsForUserById(currentUserId).Concat(postRepository.GetAllPostsSharedByUserByUserId(currentUserId)).ToList();
            List<FriendModels> friends = friendRepository.GetAllFriendsByUserId(currentUserId);

            // Make PostViewModel without ProfileModel field, to avoid interacting with ApplicationUsers
            IEnumerable<PostXmlViewModels> postsXmlViewModel = posts.Select((p) => new PostXmlViewModels() {
                PostId = p.Id,
                Text = p.Text,
                TimeAgo = p.PostDateTime
            });

            PostXmlViewModelsForUsers postXmlViewModelForUsers = new PostXmlViewModelsForUsers {
                UserId = currentUserId,
                PostList = postsXmlViewModel.ToList()
            };

            IEnumerable<FriendXmlViewModels> friendXmlViewModel = friends.Select((f) => new FriendXmlViewModels() {
                RelationId = f.Id
            });

            FriendXmlViewModelsForUsers friendXmlViewModelForUsers = new FriendXmlViewModelsForUsers {
                FriendList = friendXmlViewModel.ToList(),
                UserId = currentUserId
            };
            ProfileXmlViewModels userXmlProfileViewModels = new ProfileXmlViewModels {
                Id = userProfile.Id,
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                Gender = userProfile.Gender,
                Biography = userProfile.Biography,
                BirthDate = userProfile.BirthDate.ToShortDateString(),
                IsActive = userProfile.IsActive,
                Posts = postXmlViewModelForUsers,
                Friends = friendXmlViewModelForUsers,
                ProfileImage = userProfile.ProfileImage
            };

            string filepath = Server.MapPath("~/Content/ExportedProfiles/" + User.Identity.GetUserId() + ".xml");

            try {
                XmlSerializer serializer = new XmlSerializer(userXmlProfileViewModels.GetType());
                using (StreamWriter writer = new StreamWriter(filepath)) {
                    serializer.Serialize(writer.BaseStream, userXmlProfileViewModels);
                    writer.Flush();
                }
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/xml";
                Response.WriteFile(Server.MapPath("~/Content/ExportedProfiles/" + User.Identity.GetUserId() + ".xml"));
                Response.Flush();
                Response.End();
            } catch (Exception) {
                // Aww shieet
            }
        }
    }
}