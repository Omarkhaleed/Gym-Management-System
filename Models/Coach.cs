using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OlympicGym.Models
{
    public class Coach
    {
        [Key]
        [Display(Name = "ID")]
        public int CoachId { set; get; }

        [Required]
        [StringLength(10, ErrorMessage = "The First Name must be below 10 characters")]
        [Display(Name = "Coach")]
        public string FirstName { set; get; }

        [Required]
        [StringLength(10, ErrorMessage = "The Second Name must be below 10 characters")]
        public string SecondName { set; get; }

        [Required]
        public string Gender { set; get; }

        [Required]
        [StringLength(11, ErrorMessage = " The Phone Number must be only 11 numers")]
        public string PhoneNumber { set; get; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? DateOfStarting { set; get; }
        
        [Display(Name = "TotalTarinees")]
        public int TotalTrainees { set; get; }

        [ForeignKey("Sport")]
        [Display(Name = "Sport")]
        public int SportId { set; get; }
        public virtual Sport Sport { set; get; }

        public virtual ICollection<Trainee> Trainees { set; get; } = new HashSet<Trainee>();
        public virtual ICollection<Report> Reports { set; get; } = new HashSet<Report>();

    }
}