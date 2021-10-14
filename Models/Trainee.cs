using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OlympicGym.Models
{
    public class Trainee
    {
        [Key]
        [Display(Name = "ID")]
        public int TraineeId { set; get; }

        [Required]
        [StringLength(10,ErrorMessage ="The First Name must be below 10 characters")]
        public string FirstName { set; get; }

        [Required]
        [StringLength(10,ErrorMessage ="The Second Name must be below 10 characters")]
        public string SecondName { set; get; }

        [Required]
        [StringLength(11, ErrorMessage =" The Phone Number must be only 11 numers")]
        
        public string PhoneNumber { set; get; }

        [Required]
       

        public string Gender { set; get; }

        [Required]
        [Range(7,60, ErrorMessage = "Age must be older than 7 years old")]
        public int Age { set; get; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? DateOfRegister { set; get; }

     
        public int? Record { set; get; }

        public int classes { set; get; }

        // All Foreign Keys 
        [ForeignKey("Sport")]
        [Display(Name = "Sport")]
        public int SportId { set; get; }
        public virtual Sport Sport { set; get; }

        [ForeignKey("Plan")]
        [Display(Name = "Plan")]
        public int PlanId { set; get; }
        public virtual Plan Plan { set; get; }

        [ForeignKey("Admin")]
        [Display(Name = "Admin")]
        public int AdminId { set; get; }
        public virtual Admin Admin { set; get; }

        [ForeignKey("Coach")]
        [Display(Name = "Coach")]
        public int CoachId { set; get; }
        public virtual Coach Coach { set; get; }


    }
}