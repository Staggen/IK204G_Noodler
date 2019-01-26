using DataLayer.Models;
using DataLayer.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace NoodlerMVC.Controllers {
    public class AjaxApiController : ApiController {
        private PostRepository postRepository;
        private VisitorRepository visitorRepository;

        public AjaxApiController() {
            ApplicationDbContext context = new ApplicationDbContext();
            postRepository = new PostRepository(context);
            visitorRepository = new VisitorRepository(context);
        }

        [HttpGet]
        public List<VisitorModels> GetLatestVisitors() {
            return visitorRepository.GetLast5VisitorsByUserId(User.Identity.GetUserId());
        }

        [HttpPost]
        public void AddPost(PostModels post) {
            if (ModelState.IsValid) {
                string postTo;
                if(string.IsNullOrWhiteSpace(post.PostToId)) {
                    postTo = User.Identity.GetUserId();
                } else {
                    postTo = post.PostToId;
                }

                PostModels postModel = new PostModels() {
                    Text = post.Text,
                    PostFromId = User.Identity.GetUserId(),
                    PostToId = postTo,
                    PostDateTime = DateTime.Now
                };

                postRepository.Add(postModel);
                postRepository.Save();
            }
        }

        [HttpDelete]
        public void DeletePost(int id) {
            postRepository.Remove(id);
            postRepository.Save();
        }
    }
}