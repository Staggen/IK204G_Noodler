using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoodlerMVC.Models {
    public class PostViewModels {
        public int PostId { get; set; }
        public ProfileModels PostFrom { get; set; }

        [StringLength(280, MinimumLength = 0, ErrorMessage = "The field can only hold a maximum of 280 characters.")]
        public string Text { get; set; }

        public DateTime TimeAgo { get; set; }
    }
    public class PostViewModelsForUsers {
        public string UserId { get; set; }
        public List<PostViewModels> PostList { get; set; }
    }
}