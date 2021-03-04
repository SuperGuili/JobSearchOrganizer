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
    }
}
