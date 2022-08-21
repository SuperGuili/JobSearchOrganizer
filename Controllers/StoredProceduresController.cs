using JobSearchOrganizer.Models;
using JobSearchOrganizer.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobSearchOrganizer.Controllers
{

    public class StoredProceduresController : Controller
    {

        private readonly IJobRepository _jobRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public StoredProceduresController(UserManager<ApplicationUser> userManager, IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> ListJobs()
        {
            var jobs = _jobRepository.GetAllJobs();
            List<JobsListViewModel> models = new List<JobsListViewModel>();
            string name;

            foreach (var job in jobs)
            {
                var user = await _userManager.FindByIdAsync(job.UserID);
                name = user.UserName;

                JobsListViewModel model = new JobsListViewModel()
                {
                    job = job,                    
                    userName = name
                };

                //Add to list.
                models.Add(model);
               
            }            

            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int Id)
        {

            Job job = null;
            job = _jobRepository.GetJob(Id);
            var user = await _userManager.FindByIdAsync(job.UserID);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with id: {job.UserID} cannot be found";
                return View("NotFound");
            }
            // Method to return Role of the user by userID
            var userRole = await _userManager.GetRolesAsync(user);


            if (job == null)
            {
                Response.StatusCode = 404;
                return View("JobNotFound", Id);
            }

            JobsListViewModel model = new JobsListViewModel()
            {
                job = job,                
                userRole = userRole.FirstOrDefault(),
                userName = user.UserName           
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(JobsListViewModel model, int id)
        {
            Job job = _jobRepository.GetJob(id);

            job.InterviewDate = model.job.InterviewDate;

            _jobRepository.UpdateJob(job);            

            return RedirectToAction("ListJobs");
        }
    }
}
