using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SampleMVCApp.Controllers
{
    public class HelloController : Controller
    {
        public List<string> list;

        public HelloController()
        {
            list = new List<string>();
            list.Add("Japan");
            list.Add("USA");
            list.Add("UK");
        }
        [HttpGet("Hello/{id?}/{name?}")]
        public IActionResult Index(int id, string name)
        {
            ViewData["Message"] = "※セッションにIDとNameを保存しました。";
            HttpContext.Session.SetInt32("id", id);
            HttpContext.Session.SetString("name", name);
            return View();
        }
        [HttpGet]
        public IActionResult Other()
        {
            ViewData["id"] = HttpContext.Session.GetInt32("id");
            ViewData["name"] = HttpContext.Session.GetString("name");
            ViewData["message"] = "保存されたセッションの値を表示します。";
            return View("Index");
        }

        [HttpPost]
        public IActionResult Form()
        {
            string[] res = (string[])Request.Form["list"];
            string msg = "※";
            foreach (var item in res)
            {
                msg += "[" + item + "]";
            }

            ViewData["message"] = msg + " selected.";
            ViewData["list"] = Request.Form["list"];
            ViewData["listdata"] = list;
            return View("Index");
        }
    }
}
