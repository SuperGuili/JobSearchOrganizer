using JobSearchOrganizer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JobSearchOrganizer.ViewModels
{
    public class JobCreateViewModel
    {
        [Required]
        public string Company { get; set; }

        [NotMapped]
        public string EncryptedId { get; set; }

        [Required]
        public string JobPosition { get; set; }

        [DataType(dataType: DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = false)]
        public decimal AnnualRate { get; set; }

        [DataType(dataType: DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = false)]
        public decimal CommuteCost { get; set; }

        [DataType(dataType: DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = false)]
        public decimal Bonus { get; set; }

        public string JobLink { get; set; }

        public string Location { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email format")]
        public string ContactEmail { get; set; }

        public string ContactName { get; set; }

        public string ContactPhone { get; set; }

        public DateTime? AppliedDate { get; set; }

        public DateTime? CloseDate { get; set; }

        public DateTime? InterviewDate { get; set; }

        public DateTime? InterviewDate2 { get; set; }

        public Expectation? Expectation { get; set; }

        public JobStatus? JobStatus { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        [DataType(DataType.MultilineText)]
        public string CoverLetter { get; set; }

        [DataType(DataType.MultilineText)]
        public string Feedback { get; set; }

        [DataType(DataType.MultilineText)]
        public string JobDescription { get; set; }

        public bool IsHomeOffice { get; set; }
        public bool IsAgency { get; set; }

        public bool IsPartTime { get; set; }

        public bool IsApprentice{ get; set; }

        public IFormFile File { get; set; }
    }
}
