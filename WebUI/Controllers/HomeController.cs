using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private string message;
        private List<JobModel> jobs;

        public HomeController(IConfiguration config)
        {
            message = config["MESSAGE"] ?? "Default message here";
            jobs = new List<JobModel>();
            jobs.Add(new JobModel{Id = "1", JobType = "Job A", Started = new DateTime(2019, 9, 12, 8, 48, 26)});
            jobs.Add(new JobModel{Id = "2", JobType = "Job B", Started = DateTime.Now});
        }

        public IActionResult Index()
        {
            ViewBag.Message = message;
            return View(jobs);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
