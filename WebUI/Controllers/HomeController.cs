using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private string message;
        private readonly string cacheKey = "cachedJobs";
        private IMemoryCache cache;
        private List<JobModel> jobs;

        public HomeController(IConfiguration config, IMemoryCache memoryCache)
        {
            message = config["MESSAGE"] ?? "Default message here";
            cache = memoryCache;

            if (!cache.TryGetValue(cacheKey, out jobs))
            {
                jobs = new List<JobModel>(); // create new list and add to cache
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(3));
                cache.Set(cacheKey, jobs, cacheEntryOptions);
            }
        }

        public IActionResult Index()
        {
            ViewBag.Message = message;
            return View(jobs);
        }

        // Get a request from worker to notify of a job existence
        [HttpPost]
        public IActionResult NotifyOfAJob([FromBody]JobModel job)
        {
            if (job != null && !jobs.Exists(j => j.Id.Equals(job.Id))) jobs.Add(job);
            return NoContent();
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