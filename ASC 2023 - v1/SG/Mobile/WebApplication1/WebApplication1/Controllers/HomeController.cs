using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public FileResult download(int quizId, int typeId, bool isExpert)
        {
            var vController = new ValuesController();

            dynamic data = vController.GetData(quizId, typeId, isExpert);

            using (MemoryStream ms = new MemoryStream())
            {
                byte[] bytes = null;

                using (StreamWriter sw = new StreamWriter(ms))
                {
                    sw.WriteLine("Name,Attempt,Completion,Grade,Publish Status");

                    foreach (dynamic item in data)
                    {
                        sw.WriteLine("{0},{1},{2},{3},{4}", (item.Name).ToString().Replace("\n", ""), item.Attempt, item.Completion, item.Grade, item.Date.ToString());
                    }
                }

                bytes = ms.ToArray();

                return File(bytes, "text/csv");
            }
        }
    }
}
