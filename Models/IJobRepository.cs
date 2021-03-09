using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobSearchOrganizer.Models
{
    public interface IJobRepository
    {
        IEnumerable<Job> GetAllJobs();
        Job GetJob(int Id);
        Job AddJob(Job job);
        Job UpdateJob(Job jobChanges);
        Job Delete(int id);
    }
}
