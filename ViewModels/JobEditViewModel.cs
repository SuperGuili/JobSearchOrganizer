using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobSearchOrganizer.ViewModels
{
    public class JobEditViewModel : JobCreateViewModel
    {
        public int Id { get; set; }
        public string existingFilePath { get; set; }
    }
}
