using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Demoproject.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Demoproject.Controllers
{
    public class RegistrationController : Controller
    {
        private string path = @"E:\Project\Demoproject\Demoproject\App_Data\output.json";
        string jsondata = string.Empty;
        //string path = Server.MapPath("~/App_Data/output.json");

        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult RegistrationForm(long? clientId)
        {

            if (clientId == null || clientId == 0)
            {

            }
            else
            {
                var clientVm = new ClientVm();
                using (StreamReader read = new StreamReader(path))
                {
                    jsondata = read.ReadToEnd();
                    var list = JsonConvert.DeserializeObject<List<ClientVm>>(jsondata);
                    jsondata = JsonConvert.SerializeObject(list.Where(x => x.Id == clientId), Formatting.Indented);
                    dynamic jsonObj = JsonConvert.DeserializeObject(jsondata);
                    for (int i = 0; i < jsonObj.Count; i++)
                    {
                        clientVm = new ClientVm()
                        {
                            Id = jsonObj[i].Id,
                            SubscriptionName = jsonObj[i].SubscriptionName,
                            Email = jsonObj[i].Email,
                            Mobile = jsonObj[i].Mobile,
                            Address = jsonObj[i].Address,
                            ContactPerson = jsonObj[i].ContactPerson,
                            CreatedOn = jsonObj[i].CreatedOn
                        };
                    }
                    return View("RegistrationForm", clientVm);
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult RegistrationForm(ClientVm clientVm)
        {

            if (ModelState.IsValid)
            {
                if (clientVm.Id > 0) {
                    
                    using(StreamReader read = new StreamReader(path)){
                        jsondata = read.ReadToEnd();
                        var list = JsonConvert.DeserializeObject<List<ClientVm>>(jsondata);
                        jsondata = JsonConvert.SerializeObject(list.Where(i => i.Id == clientVm.Id), Formatting.Indented);
                        dynamic jsonObj = JsonConvert.DeserializeObject(jsondata);
                        foreach (var x in jsonObj)
                        {
                            jsonObj[0]["Address"] = clientVm.Address;
                            jsonObj[0]["Email"] = clientVm.Email;
                            jsonObj[0]["Mobile"] = clientVm.Mobile;
                            jsonObj[0]["ContactPerson"] = clientVm.ContactPerson;
                        }

                        jsondata = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);

                    }
                    System.IO.File.WriteAllText(path, jsondata);
                    return View("~/Views/Registration/Index.cshtml", clientVm);
                }
                else
                {
                    clientVm.Id = Convert.ToInt64(DateTime.UtcNow.ToString("yyyyMMddhhmmss"));
                    clientVm.CreatedOn = DateTime.Now.ToString();
                    using (StreamReader read = new StreamReader(path))
                    {
                        jsondata = read.ReadToEnd();
                        var list = JsonConvert.DeserializeObject<List<ClientVm>>(jsondata);
                        list.Add(clientVm);
                        if (list.Select(x => x.SubscriptionName).Distinct().Count() != list.Count)
                        {
                            //ViewBag.duplicateMsg = "ERROR: Sorry this Subscription Name is already taken. please try something different";
                            return Json(new { success = true, message = " ERROR: Sorry this Subscription Name is already taken. please try something different" }, JsonRequestBehavior.AllowGet);
                            //return View();
                        }
                        else
                        {
                            jsondata = JsonConvert.SerializeObject(list, Formatting.Indented);
                            ViewBag.SuccessMsg = " Registration successful...! Please check your email.";
                        }
                    }
                    System.IO.File.WriteAllText(path, jsondata);
                    return View("~/Views/Registration/Index.cshtml",clientVm);
                    //return RedirectToAction("Index", "Registration");

                }
            }
            return View();
        }

        [HttpGet]
        public JsonResult ReadJsonFile()
        {
            using (StreamReader read = new StreamReader(path))
            {
                jsondata = read.ReadToEnd();
                //dynamic array = JsonConvert.DeserializeObject(jsondata);

            }
            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeleteRecord(long? clientId)
        {
            try
            {
                using (StreamReader read = new StreamReader(path))
                {
                    jsondata = read.ReadToEnd();
                    var list = JsonConvert.DeserializeObject<List<ClientVm>>(jsondata);
                    jsondata = JsonConvert.SerializeObject(list.Where(i => i.Id != clientId), Formatting.Indented);
                }
                System.IO.File.WriteAllText(path, jsondata);
                return Json(new { success = true, message = "Your file has been deleted successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = true, message = " Delete fail" }, JsonRequestBehavior.AllowGet);
            }
        }
       
    }
}