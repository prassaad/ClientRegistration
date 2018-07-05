using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demoproject.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [HttpGet]
        public ActionResult MVCExamples(int? Id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult MVCExamples(FormCollection frm)
        {
            string Name = frm["name"].ToString();
            ViewBag.Msg = Name;
            return View();
        }
        //[HttpGet]
        //public ActionResult Santoor()
        //{ 
        //   return View("~/Views/Home/MVCExamples.cshtml");
        //}
        //[HttpPost]
        //public ActionResult Santoor(FormCollection frm)
        //{
        //    string Name = frm["name"].ToString();
        //    ViewBag.Msg = Name;
        //    return View("~/Views/Home/MVCExamples.cshtml");
        //}


    }
}