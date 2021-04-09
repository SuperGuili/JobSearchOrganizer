using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobSearchOrganizer.Models
{
    public interface IJobRepository
    {
        IEnumerable<Job> GetAllJobs();
        IEnumerable<Job> GetJobsByUser(string userId);
        Job GetJob(int Id);
        Job AddJob(Job job);
        Job UpdateJob(Job jobChanges);
        Job Delete(int id);
        IEnumerable<Job> SearchResult(string userId, string searchWord);
    }
}
