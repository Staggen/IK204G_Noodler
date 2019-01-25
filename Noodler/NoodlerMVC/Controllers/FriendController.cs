﻿using DataLayer.Models;
using DataLayer.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NoodlerMVC.Controllers {
    public class FriendController : Controller {
        private ProfileRepository profileRepository;
        private FriendRepository friendRepository;
        private RequestRepository requestRepository;

        public FriendController() {
            ApplicationDbContext context = new ApplicationDbContext();
            profileRepository = new ProfileRepository(context);
            friendRepository = new FriendRepository(context);
            requestRepository = new RequestRepository(context);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AcceptRequest(string Id) {
            string currentUserId = User.Identity.GetUserId();
            ProfileModels profile = profileRepository.Get(Id);

            if (requestRepository.IncomingRequestPending(currentUserId, profile.Id)) {
                List<RequestModels> requests = requestRepository.GetAllRequestsSentToUserById(currentUserId);
                foreach (RequestModels item in requests) {
                    if (item.RequestFromId.Equals(profile.Id) && item.RequestToId.Equals(currentUserId)) { // To find the requests between the two users
                        requestRepository.Remove(item.Id); // Remove said requests
                        requestRepository.Save(); // Save new state
                        friendRepository.Add(new FriendModels {
                            UserId = profile.Id,
                            FriendId = currentUserId
                        });
                        friendRepository.Save();
                        return Json(new { Result = true });
                    }
                }
            }
            return Json(new { Result = false });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DeclineRequest(string Id) {
            string currentUserId = User.Identity.GetUserId();
            ProfileModels profile = profileRepository.Get(Id);

            if (requestRepository.IncomingRequestPending(currentUserId, profile.Id)) {
                List<RequestModels> requests = requestRepository.GetAllRequestsSentToUserById(currentUserId);
                foreach (RequestModels item in requests) {
                    if (item.RequestFromId.Equals(profile.Id) && item.RequestToId.Equals(currentUserId)) { // To find the requests between the two users
                        requestRepository.Remove(item.Id); // Remove said requests
                        requestRepository.Save(); // Save new state
                        return Json(new { Result = true });
                    }
                }
            }
            return Json(new { Result = false });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SendRequest(string Id) {
            string currentUserId = User.Identity.GetUserId();
            ProfileModels profile = profileRepository.Get(Id);

            if (requestRepository.RequestPending(profile.Id, currentUserId)) { // If there already is a request on the way
                return Json(new { Result = false });
            }

            // If there is NOT already a request on the way
            requestRepository.Add(new RequestModels {
                RequestDateTime = DateTime.Now,
                RequestFromId = currentUserId,
                RequestToId = profile.Id
            });
            requestRepository.Save();
            return Json(new { Result = true });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult RemoveFriend(string Id) {
            string currentUserId = User.Identity.GetUserId();
            ProfileModels profile = profileRepository.Get(Id);
            List<FriendModels> friends = friendRepository.GetAllFriendsByUserId(currentUserId);
            foreach (FriendModels friend in friends) {
                if (friend.UserId.Equals(currentUserId) && friend.FriendId.Equals(profile.Id) || friend.UserId.Equals(profile.Id) && friend.FriendId.Equals(currentUserId)) {
                    friendRepository.Remove(friend.Id);
                    friendRepository.Save();
                    return Json(new { Result = true });
                }
            }
            return Json(new { Result = false });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CancelRequest(string Id) {
            string currentUserId = User.Identity.GetUserId();
            ProfileModels profile = profileRepository.Get(Id);

            if (requestRepository.OutgoingRequestPending(currentUserId, profile.Id)) {
                List<RequestModels> requests = requestRepository.GetAllRequestsSentByUserId(currentUserId);
                foreach (RequestModels item in requests) {
                    if (item.RequestFromId.Equals(currentUserId) && item.RequestToId.Equals(profile.Id)) { // To find the requests between the two users
                        requestRepository.Remove(item.Id); // Remove said requests
                        requestRepository.Save(); // Save new state
                        return Json(new { Result = true });
                    }
                }
            }
            return Json(new { Result = false });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult OutgoingRequestPending(string Id) {
            string currentUserId = User.Identity.GetUserId();
            ProfileModels profile = profileRepository.Get(Id);

            if (requestRepository.OutgoingRequestPending(currentUserId, profile.Id)) {
                return Json(new { Result = true });
            }
            return Json(new { Result = false });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult IncomingRequestPending(string Id) {
            string currentUserId = User.Identity.GetUserId();
            ProfileModels profile = profileRepository.Get(Id);

            if (requestRepository.IncomingRequestPending(currentUserId, profile.Id)) {
                return Json(new { Result = true });
            }
            return Json(new { Result = false });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult IsFriend(string Id) {
            string currentUserId = User.Identity.GetUserId();
            ProfileModels profile = profileRepository.Get(Id);
            
            if(friendRepository.IsFriendAlready(profile.Id, currentUserId)) {
                return Json(new { Result = true });
            }
            return Json(new { Result = false });
        }
    }
}