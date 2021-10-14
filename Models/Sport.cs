using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OlympicGym.Models
{
    public class Sport
    {
        [Key]
        [Display(Name = "Sport Number")]
        public int SportId { set; get; }
        [Required]
        [StringLength(10)]
        public string SportName { set; get; }

        [Display(Name = "TotalTarinees")]
        public int? TotalTrainees { set; get; }

        [Display(Name = "TotalCoaches")]
        public int? TotalCoaches { set; get; }

        public virtual ICollection<Trainee> Trainees { set; get; } = new HashSet<Trainee>();
        public virtual ICollection<Coach> Coaches { set; get; } = new HashSet<Coach>();
        public virtual ICollection<Report> Reports { set; get; } = new HashSet<Report>();
    }
}