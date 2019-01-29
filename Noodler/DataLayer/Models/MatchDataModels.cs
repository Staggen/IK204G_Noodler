using DataLayer.Repositories;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models {
    public class MatchDataModels : IIdentifiable<string> {
        [Key]
        [Display(Name = "Profile ID")]
        [ForeignKey("Profile")]
        public string Id { get; set; }
        public virtual ProfileModels Profile { get; set; }

        [Range(0, 10)]
        public int Desperation { get; set; }
        [Range(0, 10)]
        public int Loneliness { get; set; }
        [Range(0, 10)]
        public int Horniness { get; set; }
        [Range(0, 10)]
        public int Pride { get; set; }
    }
}
