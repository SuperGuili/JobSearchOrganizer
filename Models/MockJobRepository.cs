using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobSearchOrganizer.Models
{
    public class MockJobRepository : IJobRepository
    {
        private List<Job> _jobList;

        public MockJobRepository()
        {
            _jobList = new List<Job>()
            {
                new Job() {Id = 1, Company = "Company One", JobDescription = "One engineer", JobLink = "Link for 1st job", Date = DateTime.Today.ToString("d"), Expectation = Expectation.High},
                new Job() {Id = 2, Company = "Company Two", JobDescription = "Two engineer", JobLink = "Link for 2nd job", Date = DateTime.Today.ToString("d"), Expectation = Expectation.Medium},
                new Job() {Id = 3, Company = "Company Three", JobDescription = "Three engineer", JobLink = "Link for 3rd job", Date = DateTime.Today.ToString("d"), Expectation = Expectation.Medium_Low}
            };
        }

        public Job AddJob(Job job)
        {
            job.Id = _jobList.Max(e => e.Id) + 1;
            _jobList.Add(job);
            return job;
        }

        public IEnumerable<Job> GetAllJobs()
        {
            return _jobList;
        }

        public Job GetJob(int Id)
        {
            return _jobList.FirstOrDefault(j => j.Id == Id);
        }
    }
}
