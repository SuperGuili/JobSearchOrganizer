using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobSearchOrganizer.Models
{
    public class SQLJobRepository : IJobRepository
    {
        private readonly AppDbContext context;

        public SQLJobRepository(AppDbContext context)
        {
            this.context = context;
        }

        Job IJobRepository.AddJob(Job job)
        {
            context.Jobs.Add(job);
            context.SaveChanges();
            return job;
        }

        Job IJobRepository.Delete(int id)
        {
            Job job = context.Jobs.Find(id);
            if (job != null)
            {
                context.Jobs.Remove(job);
                context.SaveChanges();
            }
            return job;
        }

        IEnumerable<Job> IJobRepository.GetAllJobs()
        {
            return context.Jobs;
        }

        Job IJobRepository.GetJob(int Id)
        {
            return context.Jobs.Find(Id);
        }

        IEnumerable<Job> IJobRepository.GetJobsByUser(string userId)
        {
            var jobs = context.Jobs.Where(j => j.UserID == userId).OrderByDescending(j => j.Id);

            return jobs;
        }

        IEnumerable<Job> IJobRepository.SearchResult(string userId, string searchWord)
        {
            var jobs = context.Jobs.Where(j => j.UserID == userId).Where(j => j.Company.Contains(searchWord))
                .OrderByDescending(j => j.Id);

            return jobs;
        }

        Job IJobRepository.UpdateJob(Job jobChanges)
        {
            var job = context.Jobs.Attach(jobChanges);
            job.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return jobChanges; 
        }
    }
}
