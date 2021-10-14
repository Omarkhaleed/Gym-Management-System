using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OlympicGym.Models
{
    public class Report
    {
        [Key]
        [Display(Name = "ID")]
        public int ReportId { set; get; }

     
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime? DateOfStarting { set; get; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? DateOfEnding { set; get; }

        [Display(Name = "Trainees")]
        public int Num_Of_Trainees { set; get; }

        [Required]
        [Display(Name = " Price")]
        public int Price { set; get; }

        [Required]
        [Display(Name = "TotalPrice")]
        public int TotalPrice { set; get; }

        [ForeignKey("Coach")]
        [Display(Name = "Coach")]
        public int CoachId { set; get; }
        public virtual Coach Coach { set; get; }

        [ForeignKey("Sport")]
        [Display(Name = "Sport")]
        public int? SportId { set; get; }
        public virtual Sport Sport { set; get; }


        [ForeignKey("Admin")]
        [Display(Name = "Admin")]
        public int AdminId { set; get; }
        public virtual Admin Admin { set; get; }
    }
}