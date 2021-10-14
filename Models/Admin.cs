using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OlympicGym.Models
{
    public class Admin
    {
        [Key]
        [Display(Name = "ID")]
        public int AdminId { set; get; }

        [Required]
        [StringLength(10, ErrorMessage = "The First Name must be below 10 characters")]
        [Display(Name = "Admin")]
        public string FirstName { set; get; }

        [Required]
        [StringLength(10, ErrorMessage = "The Second Name must be below 10 characters")]
        public string SecondName { set; get; }

        [DataType(DataType.Password)]
        [Required]
        public int Password { set; get; }

        [DataType(DataType.Password)]
        [Required]
        [Compare("Password")]
        [NotMapped]
        public int ConfirmPassword { set; get; }

        [Required]
        [StringLength(11, ErrorMessage = " The Phone Number must be only 11 numers")]
        public string PhoneNumber { set; get; }

        public virtual ICollection<Trainee> Trainees { set; get; } = new HashSet<Trainee>();
        public virtual ICollection<Report> Reports { set; get; } = new HashSet<Report>();

    }
}
