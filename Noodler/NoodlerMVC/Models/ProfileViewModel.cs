using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoodlerMVC.Models {
    public class ProfileViewModel {
        public string Id { get; set; } // Foreign key.

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Biography { get; set; }

        public string BirthDate { get; set; }

        public Gender Gender { get; set; }

        public byte[] ProfileImage { get; set; }

        public bool IsActive { get; set; } // If the account is active or has been closed, default = active

        public PostViewModelsForUsers Posts { get; set; }

        public FriendViewModelsForUsers Friends { get; set; }
    }
}