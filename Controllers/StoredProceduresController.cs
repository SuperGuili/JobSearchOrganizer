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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public StoredProceduresController(RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager, IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
            _roleManager = roleManager;
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
                    userId = job.UserID,
                    userName = name
                };

                //Add to list.
                models.Add(model);
               
            }            

            return View(models);
        }

        [HttpGet]
        public IActionResult Details(int Id, string userName)
        {

            Job job = null;
            //var userId = _userManager.GetUserId(User);
            //var userRole = _roleManager.Roles.FirstOrDefault();

            job = _jobRepository.GetJob(Id);

            if (job == null)
            {
                Response.StatusCode = 404;
                return View("JobNotFound", Id);
            }

            JobsListViewModel model = new JobsListViewModel()
            {
                job = job,
                //userId = userId,
                //userRole = userRole.ToString(),
                userName = userName           
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
