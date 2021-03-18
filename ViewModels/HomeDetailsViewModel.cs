using JobSearchOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobSearchOrganizer.ViewModels
{
    public class HomeDetailsViewModel
    {
        public Job Job { get; set; }

        public string PageTitle { get; set; }

        public bool Applied { get; set; }

        public bool Researched { get; set; }

        public bool FollowUpSent { get; set; }

        public bool Interviewed { get; set; }

        public bool FollowUp2Sent { get; set; }

        public bool Finished { get; set; }
    }
}
