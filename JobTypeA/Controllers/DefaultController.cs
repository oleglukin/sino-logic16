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
            return "JobTypeA is working";
        }


        [HttpPost]
        public void Start([FromBody] string jsonValue)
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

            DirectoryInfo d = new DirectoryInfo(sourceFolder);
            foreach (var file in d.GetFiles($"*.{fileExtension}"))
            {
                System.IO.File.Move(file.FullName, $"{targetFolder}/{file.Name}");
            }
        }
    }
}
