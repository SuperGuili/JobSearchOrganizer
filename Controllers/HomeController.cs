using JobSearchOrganizer.Models;
using JobSearchOrganizer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobSearchOrganizer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IJobRepository _jobRepository;

        public HomeController(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }
        public ViewResult Index()
        {
            var model = _jobRepository.GetAllJobs();
            return View(model);
        }
        public ViewResult Details(int? Id)
        {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Job = _jobRepository.GetJob(Id??1),
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
        public IActionResult Create(Job job)
        {
            if (ModelState.IsValid)
            {
                Job newJob = _jobRepository.AddJob(job);
                return RedirectToAction("Details", new { id = newJob.Id }); 
            }
            return View();
        }
    }
}
