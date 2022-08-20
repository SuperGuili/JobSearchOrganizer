using JobSearchOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobSearchOrganizer.ViewModels
{
    public class JobsListViewModel
    {
        public Job job { get; set; }

        public string userId { get; set; }

        public string userName { get; set; }

        public string userRole { get; set; }



    }
}
