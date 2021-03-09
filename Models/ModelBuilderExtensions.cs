using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobSearchOrganizer.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //table to be seeded
            //modelBuilder.Entity<Job>().HasData(
            //    new Job
            //    {
            //        Id = 1,
            //        Company = "One",
            //        JobDescription = "Developer",
            //        JobLink = "www.link.com",
            //        JobEmail = "email@gmail.com",
            //        Date = "01/03/2021",
            //        Expectation = Expectation.High
            //    }
            //);
        }
    }
}
