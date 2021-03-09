using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobSearchOrganizer.Models
{
    public class Job
    {
        public int Id { get; set; }

        public string UserName { get; set; }

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
