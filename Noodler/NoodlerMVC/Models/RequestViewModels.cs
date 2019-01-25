using System;

namespace NoodlerMVC.Models {
    public class RequestViewModels {
        public int RequestId { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public DateTime RequestDateTime { get; set; }
        public byte[] ProfileImage { get; set; }
    }
}