using DataLayer.Repositories;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models {
    public class PostModels : IIdentifiable<int> { // Needs extension to be able to take replies to post.
        [Key]
        public int Id { get; set; }

        [StringLength(280, MinimumLength = 0, ErrorMessage = "The field can only hold a maximum of 280 characters.")]
        public string Text { get; set; }

        public DateTime PostDateTime { get; set; }

        [ForeignKey("PostFrom")]
        public string PostFromId { get; set; }
        public virtual ProfileModels PostFrom { get; set; }

        [ForeignKey("PostTo")]
        public string PostToId { get; set; }
        public virtual ProfileModels PostTo { get; set; }
    }
}
