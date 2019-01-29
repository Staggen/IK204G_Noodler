using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoodlerMVC.Controllers {
    [Authorize]
    public class MatchDataController : Controller {
        private MatchDataRepository matchDataRepository;

        public MatchDataController() {
            ApplicationDbContext context = new ApplicationDbContext();
            matchDataRepository = new MatchDataRepository(context);
        }
    }
}