using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleMVCApp.Controllers
{
    public class HelloController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Message"] = "Hello! this is sample message!";
            ViewData["name"] = "";
            ViewData["mail"] = "";
            ViewData["tel"] = "";
            return View();
        }

        [HttpPost]
        public IActionResult Form()
        {
            ViewData["name"] = Request.Form["name"];
            ViewData["mail"] = Request.Form["mail"];
            ViewData["tel"] = Request.Form["tel"];
            ViewData["Message"] = ViewData["name"] + ", " + ViewData["mail"] + ", " + ViewData["tel"];
            return View("Index");
        }
    }
}
