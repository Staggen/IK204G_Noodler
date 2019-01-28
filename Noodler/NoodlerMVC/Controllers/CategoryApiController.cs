using DataLayer.Models;
using DataLayer.Repositories;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web.Http;

namespace NoodlerMVC.Controllers {
    public class CategoryApiController : ApiController {
        private FriendCategoryRepository friendCategoryRepository;
        private FriendRepository friendRepository;

        public CategoryApiController() {
            ApplicationDbContext context = new ApplicationDbContext();
            friendCategoryRepository = new FriendCategoryRepository(context);
            friendRepository = new FriendRepository(context);
        }

        [HttpPost]
        [ActionName("Add")]
        public void AddCategory(FriendCategoryModels category) {
            if (ModelState.IsValid) {
                if (category.CategoryName.Length >= 1 && category.CategoryName.Length <= 20) {
                    friendCategoryRepository.Add(category);
                    friendCategoryRepository.Save();
                }
            }
        }

        [HttpPost]
        [ActionName("Edit")]
        public void EditCategory(FriendCategoryModels category) {
            if (ModelState.IsValid) {
                friendCategoryRepository.Edit(category);
                friendCategoryRepository.Save();
            }
        }

        [HttpDelete]
        [ActionName("Delete")]
        public void DeleteCategory(int Id) { // Check all friend relations and replace the FriendCategory to the default one where it was removed.
            if (Id != 1) {
                List<FriendModels> list = friendRepository.GetAllFriendsByFriendCategoryId(Id);
                foreach(var item in list) {
                    item.FriendCategory = 1;
                    friendRepository.Edit(item);
                }
                friendRepository.Save();
                friendCategoryRepository.Remove(Id);
                friendCategoryRepository.Save();
            }
            
        }
    }
}
