using DataLayer.Models;
using System;
using System.Collections.Generic;

namespace NoodlerMVC.Models {
    public class FriendXmlViewModels {
        public int RelationId { get; set; }
    }

    public class FriendXmlViewModelsForUsers {
        public string UserId { get; set; }
        public List<FriendXmlViewModels> FriendList { get; set; }
    }

    public class PostXmlViewModels {
        public int PostId { get; set; }

        public string Text { get; set; }

        public DateTime TimeAgo { get; set; }
    }

    public class PostXmlViewModelsForUsers {
        public string UserId { get; set; }
        public List<PostXmlViewModels> PostList { get; set; }
    }

    public class ProfileXmlViewModels {
        public string Id { get; set; } // Foreign key.

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Biography { get; set; }

        public string BirthDate { get; set; }

        public Gender Gender { get; set; }

        public byte[] ProfileImage { get; set; }

        public bool IsActive { get; set; } // If the account is active or has been closed, default = active

        public PostXmlViewModelsForUsers Posts { get; set; }

        public FriendXmlViewModelsForUsers Friends { get; set; }
    }
}