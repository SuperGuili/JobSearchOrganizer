using JobSearchOrganizer.Models;
using JobSearchOrganizer.Security;
using JobSearchOrganizer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDataProtector protector;     

        public HomeController(IJobRepository jobRepository, IWebHostEnvironment hostingEnvironment,
            UserManager<ApplicationUser> userManager, IDataProtectionProvider dataProtectionProvider,
            DataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            _jobRepository = jobRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;            
            protector = dataProtectionProvider.CreateProtector(dataProtectionPurposeStrings.JobIdRouteValue);
        }

        [AllowAnonymous]
        public ViewResult Welcome()
        {
            return View();
        }

        public IActionResult Index()
        {
            // filter by userId
            var userId = userManager.GetUserId(User);

            var jobs = _jobRepository.GetJobsByUser(userId);

            if (jobs.Count() > 0)
            {
                foreach (var job in jobs)
                {
                    job.EncryptedId = protector.Protect(job.Id.ToString());
                }
            }

            return View(jobs);
        }

        [HttpGet]
        public ViewResult Details(string Id)
        {
            int decryptedId = 0;
            Job job = null;
            try
            {
                 decryptedId = Convert.ToInt32(protector.Unprotect(Id));
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return View("JobNotFound", decryptedId);
                //throw;
            }

            if (decryptedId != 0)
            {
                job = _jobRepository.GetJob(decryptedId);

                if (job == null)
                {
                    Response.StatusCode = 404;
                    return View("JobNotFound", decryptedId);
                } 
            }

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Job = job,
                PageTitle = "Job Details"
            };
            job.EncryptedId = protector.Protect(job.Id.ToString());

            switch (job.JobStatus)
            {
                case JobStatus.None:
                    homeDetailsViewModel.Applied = false;
                    homeDetailsViewModel.Researched = false;
                    homeDetailsViewModel.FollowUpSent = false;
                    homeDetailsViewModel.Interviewed = false;
                    homeDetailsViewModel.FollowUp2Sent = false;
                    homeDetailsViewModel.Finished = false;
                    break;

                case JobStatus.Applied:
                    homeDetailsViewModel.Applied = true;
                    homeDetailsViewModel.Researched = false;
                    homeDetailsViewModel.FollowUpSent = false;
                    homeDetailsViewModel.Interviewed = false;
                    homeDetailsViewModel.FollowUp2Sent = false;
                    homeDetailsViewModel.Finished = false;
                    break;

                case JobStatus.Researched:
                    homeDetailsViewModel.Applied = true;
                    homeDetailsViewModel.Researched = true;
                    homeDetailsViewModel.FollowUpSent = false;
                    homeDetailsViewModel.Interviewed = false;
                    homeDetailsViewModel.FollowUp2Sent = false;
                    homeDetailsViewModel.Finished = false;
                    break;

                case JobStatus.FollowUpSent:
                    homeDetailsViewModel.Applied = true;
                    homeDetailsViewModel.Researched = true;
                    homeDetailsViewModel.FollowUpSent = true;
                    homeDetailsViewModel.Interviewed = false;
                    homeDetailsViewModel.FollowUp2Sent = false;
                    homeDetailsViewModel.Finished = false;
                    break;

                case JobStatus.Interviewed:
                    homeDetailsViewModel.Applied = true;
                    homeDetailsViewModel.Researched = true;
                    homeDetailsViewModel.FollowUpSent = true;
                    homeDetailsViewModel.Interviewed = true;
                    homeDetailsViewModel.FollowUp2Sent = false;
                    homeDetailsViewModel.Finished = false;
                    break;

                case JobStatus.FollowUp2Sent:
                    homeDetailsViewModel.Applied = true;
                    homeDetailsViewModel.Researched = true;
                    homeDetailsViewModel.FollowUpSent = true;
                    homeDetailsViewModel.Interviewed = true;
                    homeDetailsViewModel.FollowUp2Sent = true;
                    homeDetailsViewModel.Finished = false;
                    break;

                case JobStatus.Finished:
                    homeDetailsViewModel.Applied = true;
                    homeDetailsViewModel.Researched = true;
                    homeDetailsViewModel.FollowUpSent = true;
                    homeDetailsViewModel.Interviewed = true;
                    homeDetailsViewModel.FollowUp2Sent = true;
                    homeDetailsViewModel.Finished = true;
                    break;

                default:
                    break;
            }

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
                    UserID = userManager.GetUserId(User),
                    Company = model.Company,
                    JobPosition = model.JobPosition,
                    JobLink = model.JobLink,
                    ContactEmail = model.ContactEmail,
                    ContactPhone = model.ContactPhone,
                    AppliedDate = model.AppliedDate,
                    CloseDate = model.CloseDate,
                    Expectation = model.Expectation,
                    AnnualRate = model.AnnualRate,
                    CommuteCost = model.CommuteCost,
                    Bonus = model.Bonus,
                    JobStatus = 0,
                    NextStep = JobStatus.Apply,
                    Location = model.Location,
                    InterviewDate = model.InterviewDate,
                    InterviewDate2 = null,
                    Notes = null,
                    CoverLetter = null,
                    Feedback = null,
                    IsAgency = model.IsAgency,
                    IsHomeOffice = model.IsHomeOffice,
                    FilePath = uniqueFileName
                };

                _jobRepository.AddJob(newJob);

                newJob.EncryptedId = protector.Protect(newJob.Id.ToString());

                return RedirectToAction("Details", new { id = newJob.EncryptedId });
            }
            return View();
        }

        [HttpGet]
        public ViewResult Edit(string id)
        {
            int decryptedId = 0;
            Job job = null;
            try
            {
                decryptedId = Convert.ToInt32(protector.Unprotect(id));
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return View("JobNotFound", decryptedId);                
            }

            if (decryptedId != 0)
            {
                job = _jobRepository.GetJob(decryptedId);

                if (job == null)
                {
                    Response.StatusCode = 404;
                    return View("JobNotFound", decryptedId);
                }
            }           

            JobEditViewModel jobEditViewModel = new JobEditViewModel
            {
                Id = job.Id,
                EncryptedId = id,
                Company = job.Company,
                JobPosition = job.JobPosition,
                AnnualRate = job.AnnualRate,
                CommuteCost = job.CommuteCost,
                Bonus = job.Bonus,
                JobLink = job.JobLink,
                Location = job.Location,
                ContactEmail = job.ContactEmail,
                ContactPhone = job.ContactPhone,
                AppliedDate = job.AppliedDate,
                CloseDate = job.CloseDate,
                InterviewDate = job.InterviewDate,
                InterviewDate2 = job.InterviewDate2,
                JobStatus = job.JobStatus,
                NextStep = (JobStatus)job.NextStep,
                Notes = job.Notes,
                CoverLetter = job.CoverLetter,
                Feedback = job.Feedback,
                Expectation = job.Expectation,
                IsAgency = job.IsAgency,
                IsHomeOffice = job.IsHomeOffice
            };

            switch (job.JobStatus)
            {
                case JobStatus.None:
                    jobEditViewModel.Applied = false;
                    jobEditViewModel.Researched = false;
                    jobEditViewModel.FollowUpSent = false;
                    jobEditViewModel.Interviewed = false;
                    jobEditViewModel.FollowUp2Sent = false;
                    jobEditViewModel.Finished = false;
                    break;

                case JobStatus.Applied:
                    jobEditViewModel.Applied = true;
                    jobEditViewModel.Researched = false;
                    jobEditViewModel.FollowUpSent = false;
                    jobEditViewModel.Interviewed = false;
                    jobEditViewModel.FollowUp2Sent = false;
                    jobEditViewModel.Finished = false;
                    break;

                case JobStatus.Researched:
                    jobEditViewModel.Applied = true;
                    jobEditViewModel.Researched = true;
                    jobEditViewModel.FollowUpSent = false;
                    jobEditViewModel.Interviewed = false;
                    jobEditViewModel.FollowUp2Sent = false;
                    jobEditViewModel.Finished = false;
                    break;

                case JobStatus.FollowUpSent:
                    jobEditViewModel.Applied = true;
                    jobEditViewModel.Researched = true;
                    jobEditViewModel.FollowUpSent = true;
                    jobEditViewModel.Interviewed = false;
                    jobEditViewModel.FollowUp2Sent = false;
                    jobEditViewModel.Finished = false;
                    break;

                case JobStatus.Interviewed:
                    jobEditViewModel.Applied = true;
                    jobEditViewModel.Researched = true;
                    jobEditViewModel.FollowUpSent = true;
                    jobEditViewModel.Interviewed = true;
                    jobEditViewModel.FollowUp2Sent = false;
                    jobEditViewModel.Finished = false;
                    break;

                case JobStatus.FollowUp2Sent:
                    jobEditViewModel.Applied = true;
                    jobEditViewModel.Researched = true;
                    jobEditViewModel.FollowUpSent = true;
                    jobEditViewModel.Interviewed = true;
                    jobEditViewModel.FollowUp2Sent = true;
                    jobEditViewModel.Finished = false;
                    break;

                case JobStatus.Finished:
                    jobEditViewModel.Applied = true;
                    jobEditViewModel.Researched = true;
                    jobEditViewModel.FollowUpSent = true;
                    jobEditViewModel.Interviewed = true;
                    jobEditViewModel.FollowUp2Sent = true;
                    jobEditViewModel.Finished = true;
                    break;

                default:
                    break;
            }

            return View(jobEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(JobEditViewModel model)
        {
            Job job = _jobRepository.GetJob(model.Id);

            string EncryptedId = protector.Protect(model.Id.ToString());

            model.EncryptedId = EncryptedId;

            if (ModelState.IsValid)
            {
                job.EncryptedId = EncryptedId;
                job.Company = model.Company;
                job.JobPosition = model.JobPosition;
                job.AnnualRate = model.AnnualRate;
                job.Bonus = model.Bonus;
                job.CommuteCost = model.CommuteCost;
                job.JobLink = model.JobLink;
                job.Location = model.Location;
                job.ContactEmail = model.ContactEmail;
                job.ContactPhone = model.ContactPhone;
                job.AppliedDate = model.AppliedDate;
                job.CloseDate = model.CloseDate;
                job.InterviewDate = model.InterviewDate;
                job.InterviewDate2 = model.InterviewDate2;
                job.Notes = model.Notes;
                job.CoverLetter = model.CoverLetter;
                job.Feedback = model.Feedback;
                job.JobStatus = model.JobStatus;
                job.Expectation = model.Expectation;
                job.IsAgency = model.IsAgency;
                job.IsHomeOffice = model.IsHomeOffice;

                if (model.Applied == true)
                {
                    job.JobStatus = JobStatus.Applied;
                    job.NextStep = JobStatus.Research;

                    if (model.Researched == true)
                    {
                        job.JobStatus = JobStatus.Researched;
                        job.NextStep = JobStatus.SendFollowUPEmail;

                        if (model.FollowUpSent == true)
                        {
                            job.JobStatus = JobStatus.FollowUpSent;
                            job.NextStep = JobStatus.Interview;

                            if (model.Interviewed == true)
                            {
                                job.JobStatus = JobStatus.Interviewed;
                                job.NextStep = JobStatus.SendFollowUP2;

                                if (model.FollowUp2Sent == true)
                                {
                                    job.JobStatus = JobStatus.FollowUp2Sent;
                                    job.NextStep = JobStatus.Finished;

                                    if (model.Finished == true)
                                    {
                                        job.JobStatus = JobStatus.Finished;
                                        job.NextStep = JobStatus.None;
                                    }
                                }
                            } 
                        }                        
                    }                    
                }
                if(model.Applied == false)
                {
                    job.JobStatus = JobStatus.None;
                    job.NextStep = JobStatus.Apply;
                }                

                _jobRepository.UpdateJob(job);

                return RedirectToAction("Details", new { id = EncryptedId });
            }

            return View();
        }

        [HttpPost]
        public IActionResult DeleteJob(int id)
        {
            Job job = _jobRepository.GetJob(id);

            if (job == null)
            {
                Response.StatusCode = 404;
                return View("JobNotFound", id);
            }

            _jobRepository.Delete(id);

            return RedirectToAction("Index");
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
