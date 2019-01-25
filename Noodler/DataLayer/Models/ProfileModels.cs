using DataLayer.Repositories;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models {
    public class ProfileModels : IIdentifiable<string> {
        [Key]
        [Display(Name = "Profile ID")]
        [ForeignKey("User")]
        public string Id { get; set; } // Foreign key.
        public virtual ApplicationUser User { get; set; } // Foreign key target.

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You need to have a first name.")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Your first name must be at least 2 characters long.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You need to have a last name.")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Your last name must be at least 2 characters long.")]
        public string LastName { get; set; }

        [StringLength(300, ErrorMessage = "Your biography may only be 300 characters long.")]
        public string Biography { get; set; }

        [Display(Name = "Birthdate")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "You need to have a birthdate.")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "You need to have a gender.")]
        public Gender Gender { get; set; }

        [Display(Name = "Profile Image")]
        public byte[] ProfileImage { get; set; }

        public bool IsActive { get; set; } = true; // If the account is active or has been closed, default = active

        // Add further things onto the model if necessary, possibly make a third model for matches rather than adding such data onto this one?

    }

    public enum Gender {
        Man,
        Woman,
        Attackhelicopter,
        Other
    }
}
