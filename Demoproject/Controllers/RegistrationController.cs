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
        private string path = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/output.json"); 
        string jsondata = string.Empty;

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
                ViewBag.ButtonName = "Submit";
            }
            else
            {
                ViewBag.ButtonName = "Update";
                var tenant = new Tenant();
                using (StreamReader read = new StreamReader(path))
                {
                    jsondata = read.ReadToEnd();
                    var appSettingsRoot = JsonConvert.DeserializeObject<AppSettings>(jsondata);
                    List<Tenant> tenants = appSettingsRoot.Multitenancy.Tenants;
                    jsondata = JsonConvert.SerializeObject(tenants.Where(x => x.Id == clientId), Formatting.Indented);
                    dynamic jsonObj = JsonConvert.DeserializeObject(jsondata);
                    tenant = new Tenant()
                    {
                        Id = jsonObj[0].Id,
                        Name = jsonObj[0].Name,
                        Email = jsonObj[0].Email,
                        Mobile = jsonObj[0].Mobile,
                        Address = jsonObj[0].Address,
                        ContactPerson = jsonObj[0].ContactPerson,
                        CreatedOn = jsonObj[0].CreatedOn
                    };
                    return View("RegistrationForm", tenant);
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult RegistrationForm(Tenant tenant, FormCollection frm)
        {
            ViewBag.ButtonName = "Submit";
            var hostname = frm["hostname"] + "." + "mesure.io";

            if (ModelState.IsValid)
            {
                if (tenant.Id > 0)
                {
                    using (StreamReader read = new StreamReader(path))
                    {
                        jsondata = read.ReadToEnd();
                        var jObject = JObject.Parse(jsondata);
                        JArray tenantsArray = (JArray)jObject["Multitenancy"]["Tenants"];
                        foreach (var client in tenantsArray.Where(obj => obj["Id"].Value<long>() == tenant.Id))
                        {
                            client["Name"] = tenant.Name;
                            client["Address"] = tenant.Address;
                            client["Email"] = tenant.Email;
                            client["Mobile"] = tenant.Mobile;
                            client["ContactPerson"] = tenant.ContactPerson;
                        }
                        jObject["Multitenancy"]["Tenants"] = tenantsArray;
                        jsondata = JsonConvert.SerializeObject(jObject, Formatting.Indented);
                        ViewBag.SuccessMsg = " Update successful...!";
                    }
                    System.IO.File.WriteAllText(path, jsondata);
                    return View("~/Views/Registration/Index.cshtml", tenant);
                }
                else
                {
                    tenant.Id = Convert.ToInt64(DateTime.UtcNow.ToString("yyMMddhhmmss"));
                    tenant.CreatedOn = DateTime.Now.ToShortDateString();
                    tenant.Services = new List<string> { "Admint", "Client" };
                    tenant.Hostnames = new List<string> { hostname };
                    tenant.Name = tenant.Name;
                    tenant.Theme = "Cerulean";
                    tenant.ConnectionString = "Godaddy";
                    using (StreamReader read = new StreamReader(path))
                    {
                        jsondata = read.ReadToEnd();
                        var appSettingsRoot = JsonConvert.DeserializeObject<AppSettings>(jsondata);
                        List<Tenant> tenants = appSettingsRoot.Multitenancy.Tenants;
                        if (tenants.Any(x => x.Hostnames.Contains(hostname)) || tenants.Any(x => x.Email.Contains(tenant.Email)))
                        {
                            ViewBag.duplicateMsg = " Sorry ! this Subscription Name is already taken or Email is associated. Please try with other Subscription name or Email.";
                            return View();
                        }
                        else
                        {
                            appSettingsRoot.Multitenancy.Tenants.Add(tenant);
                            jsondata = JsonConvert.SerializeObject(appSettingsRoot, Formatting.Indented);
                            ViewBag.SuccessMsg = " Registration successful...! Please check your email.";
                        }
                    }
                    System.IO.File.WriteAllText(path, jsondata);
                    return View("~/Views/Registration/Index.cshtml", tenant);
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
                var appSettingsRoot = JsonConvert.DeserializeObject<AppSettings>(jsondata);
                List<Tenant> tenants = appSettingsRoot.Multitenancy.Tenants;
                jsondata = JsonConvert.SerializeObject(tenants, Formatting.Indented);
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
                    var jObject = JObject.Parse(jsondata);
                    JArray tenantsArray = (JArray)jObject["Multitenancy"]["Tenants"];
                    var companyToDeleted = tenantsArray.FirstOrDefault(obj => obj["Id"].Value<long>() == clientId);
                    tenantsArray.Remove(companyToDeleted);
                    jsondata = JsonConvert.SerializeObject(jObject, Formatting.Indented);
                }
                System.IO.File.WriteAllText(path, jsondata);
                return Json(new { success = true, message = "Your file has been deleted successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false, message = " Delete Failed" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}