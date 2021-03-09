using JobSearchOrganizer.Models;
using JobSearchOrganizer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JobSearchOrganizer.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IJobRepository _jobRepository;
        private readonly IWebHostEnvironment hostingEnvironment;

        public HomeController(IJobRepository jobRepository, IWebHostEnvironment hostingEnvironment)
        {
            _jobRepository = jobRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        [AllowAnonymous]
        public ViewResult Welcome()
        {
            return View();
        }

        public ViewResult Index()
        {
            // filter by user

            var jobs = _jobRepository.GetAllJobs();
            var model = new List<Job>();

            foreach (var job in jobs)
            {
                if (job.UserName == User.Identity.Name)
                {
                    model.Add(job);
                }                
            }

            return View(model);
        }
        public ViewResult Details(int? Id)
        {
            Job job = _jobRepository.GetJob(Id.Value);

            if (job == null)
            {
                Response.StatusCode = 404;
                return View("JobNotFound", Id.Value);
            }

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Job = job,
                PageTitle = "Job Details"
            };

            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(JobCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);

                Job newJob = new Job
                {
                    UserName = User.Identity.Name,
                    Company = model.Company,
                    JobDescription = model.JobDescription,
                    JobLink = model.JobLink,
                    JobEmail = model.JobEmail,
                    Date = model.Date,
                    Expectation = model.Expectation,
                    FilePath = uniqueFileName
                };
                _jobRepository.AddJob(newJob);
                return RedirectToAction("Details", new { id = newJob.Id });
            }
            return View();
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Job job = _jobRepository.GetJob(id);
            JobEditViewModel jobEditViewModel = new JobEditViewModel
            {
                Id = job.Id,
                Company = job.Company,
                JobDescription = job.JobDescription,
                JobLink = job.JobLink,
                JobEmail = job.JobEmail,
                Date = job.Date,
                Expectation = job.Expectation,
                existingFilePath = job.FilePath
            };

            return View(jobEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(JobEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Job job = _jobRepository.GetJob(model.Id);
                job.Company = model.Company;
                job.JobDescription = model.JobDescription;
                job.JobLink = model.JobLink;
                job.JobEmail = model.JobEmail;
                job.Date = model.Date;
                job.Expectation = model.Expectation;

                if (model.File != null)
                {
                    if (model.existingFilePath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "files", model.existingFilePath);
                        System.IO.File.Delete(filePath);
                    }
                    job.FilePath = ProcessUploadedFile(model);
                }

                _jobRepository.UpdateJob(job);
                return RedirectToAction("Details", new { id = job.Id });
            }
            return View();
        }

        private string ProcessUploadedFile(JobCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.File != null)
            {
                string uploadFileFolder = Path.Combine(hostingEnvironment.WebRootPath, "files");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.File.FileName;
                string filePath = Path.Combine(uploadFileFolder, uniqueFileName);

                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    model.File.CopyTo(filestream);
                }                
            }
            return uniqueFileName;
        }
    }
}
