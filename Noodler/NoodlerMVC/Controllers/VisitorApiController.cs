using DataLayer.Models;
using DataLayer.Repositories;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web.Http;

namespace NoodlerMVC.Controllers {
    public class VisitorApiController : ApiController {
        private VisitorRepository visitorRepository;

        public VisitorApiController() {
            ApplicationDbContext context = new ApplicationDbContext();
            visitorRepository = new VisitorRepository(context);
        }

        [HttpGet]
        public List<VisitorModels> GetLatestVisitors() {
            return visitorRepository.GetLast5VisitorsByUserId(User.Identity.GetUserId());
        }
    }
}