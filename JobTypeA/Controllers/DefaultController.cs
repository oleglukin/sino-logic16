using Newtonsoft.Json.Linq;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace JobTypeA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        // Check if the job is still alive
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "JobTypeA is working. It moves all files of a certain type from one folder to another";
        }

        
        /// <summary>
        /// Do the job. Accept Json payload to overwrite default parameters
        /// </summary>
        [HttpPost]
        public ActionResult<string> Start([FromBody] string jsonValue)
        {
            JObject jo = JObject.Parse(jsonValue);

            string sourceFolder = "/volume/source";
            if (jo.ContainsKey("sourcefolder"))
            {
                sourceFolder = jo["sourcefolder"].ToString();
            }

            string targetFolder = "/volume/target";
            if (jo.ContainsKey("targetfolder"))
            {
                targetFolder = jo["targetfolder"].ToString();
            }

            string fileExtension = "json";
            if (jo.ContainsKey("fileextension"))
            {
                fileExtension = jo["fileextension"].ToString();
            }

            int files = 0;
            DirectoryInfo d = new DirectoryInfo(sourceFolder);
            foreach (var file in d.GetFiles($"*.{fileExtension}"))
            {
                System.IO.File.Move(file.FullName, $"{targetFolder}/{file.Name}");
                files++;
            }

            return $"The job is finished. Moved {files} {fileExtension} files from {sourceFolder} to {targetFolder}.";
        }
    }
}
