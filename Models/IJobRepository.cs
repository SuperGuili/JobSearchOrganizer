using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobSearchOrganizer.Models
{
    public interface IJobRepository
    {
        Job GetJob(int Id);
        IEnumerable<Job> GetAllJobs();

        Job AddJob(Job job);
    }
}
