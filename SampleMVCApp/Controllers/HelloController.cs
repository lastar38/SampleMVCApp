using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SampleMVCApp.Controllers
{
    public class HelloController : Controller
    {
        [HttpGet("Hello/{id?}/{name?}")]
        public IActionResult Index(int id, string name)
        {
            ViewData["Message"] = "※セッションにIDとNameを保存しました。";
            MyData ob = new MyData(id, name);
            HttpContext.Session.Set("object", ObjectToBytes(ob));
            ViewData["object"] = ob;
            return View();
        }
        [HttpGet]
        public IActionResult Other()
        {
            ViewData["message"] = "保存されたセッションの値を表示します。";
            byte[] ob = HttpContext.Session.Get("object");
            ViewData["object"] = BytesToObject(ob);
            return View("Index");
        }

        private byte[] ObjectToBytes(Object ob)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, ob);
            return ms.ToArray();
        }

        private Object BytesToObject(byte[] arr)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            ms.Write(arr, 0, arr.Length);
            ms.Seek(0, SeekOrigin.Begin);
            return (Object)bf.Deserialize(ms);
        }
    }

    [Serializable]
    class MyData
    {
        public int Id = 0;
        public string Name = "";
        public MyData(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public override string ToString()
        {
            return "<" + Id + ": " + Name + ">";
        }
    }

}
