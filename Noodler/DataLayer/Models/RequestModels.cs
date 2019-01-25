using DataLayer.Repositories;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models {
    public class RequestModels : IIdentifiable<int> {
        [Key]
        public int Id { get; set; }

        public DateTime RequestDateTime { get; set; }

        [ForeignKey("RequestFrom")]
        public string RequestFromId { get; set; }
        public virtual ProfileModels RequestFrom { get; set; }

        [ForeignKey("RequestTo")]
        public string RequestToId { get; set; }
        public virtual ProfileModels RequestTo { get; set; }
    }
}
