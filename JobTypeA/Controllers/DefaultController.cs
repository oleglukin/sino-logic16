using System;
using Microsoft.AspNetCore.Mvc;

namespace JobTypeA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        // GET api/value
        // Check if the job is still alive
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "JobTypeA is working";
        }

        // POST api/values
        [HttpPost]
        public void Start([FromBody] string value)
        {
            Console.WriteLine("Invoked Start() method");
        }
    }
}
