using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobSearchOrganizer.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string City { get; set; }

        //public string UserID { get; set; }

        public Job Job { get; set; }
    }
}
