using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OlympicGym.Models
{
    public class Plan
    {
        [Key]
        [Display (Name="PlanNumber")]
        public int PlanId { set; get; }

        [Required]
        [Display(Name = "PlanName")]
        [StringLength(20, ErrorMessage = " The Plan Name must be below 20 characters")]
        public string PlanName { set; get; }

        [Display(Name = "Classes")]
        [Required]
        public int PlanClasses { set; get; }

        [Required]
        [Display(Name ="Description")]
        [DataType(DataType.Text)]
        public string PlanDescription { set; get; }

        [Required]
        [Display(Name = " Price")]
        public int PlanPrice { set; get; }

        public virtual ICollection<Trainee> Trainees { set; get; } = new HashSet<Trainee>();
    }
}