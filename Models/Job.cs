using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JobSearchOrganizer.Models
{
    public class Job
    {
        public int Id { get; set; }

        [NotMapped]
        public string EncryptedId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(450)")]
        public string UserID { get; set; }

        [ForeignKey("UserID")]
        public ApplicationUser User { get; set; }

        [Required]
        public string Company { get; set; }
        public string JobDescription { get; set; }
        public string JobLink { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email format")]
        public string JobEmail { get; set; }
        public string Date { get; set; }

        [Required]
        public Expectation? Expectation { get; set; }

        public string FilePath { get; set; }
    }
}
