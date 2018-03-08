using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace CustomerWebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment env;

        public HomeController(IHostingEnvironment env)
        {
            this.env = env;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var path = Path.Combine(this.env.WebRootPath, "index.html");
            var bytes = System.IO.File.ReadAllBytes(path);
            return new FileContentResult(bytes, "text/html");
        }
    }
}
