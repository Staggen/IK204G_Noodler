using DataLayer.Repositories;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models {
    public class VisitorModels : IIdentifiable<int> {
        [Key]
        public int Id { get; set; }

        public DateTime VisitDateTime { get; set; }

        [ForeignKey("VisitFrom")]
        public string VisitFromId { get; set; }
        public virtual ProfileModels VisitFrom { get; set; }

        [ForeignKey("VisitTo")]
        public string VisitToId { get; set; }
        public virtual ProfileModels VisitTo { get; set; }
    }
}
