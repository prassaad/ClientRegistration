using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Demoproject.Models
{
    public class ClientVm
    {
        public  Nullable<long> Id { get; set; }

        [Required(ErrorMessage = "Please Enter Subscription Name")]
        [Display(Name = "Subscription Name")]
        public string SubscriptionName { get; set; }

        [Required(ErrorMessage = "Please Enter Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please Enter Email Address")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        ErrorMessage = "Please Enter Correct Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Mobile No")]
        [StringLength(10, ErrorMessage = "The Mobile must contains 10 characters", MinimumLength = 10)]
        public string Mobile { get; set; }

        [Required(ErrorMessage ="Please Enter the Contact Person Name")]
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }

        public string CreatedOn { get; set; }

    }
    public class DefaultConnection
    {
        public string ConnectionString { get; set; }
    }

    public class Data
    {
        public DefaultConnection DefaultConnection { get; set; }
    }

    public class LogLevel
    {
        public string Default { get; set; }
        public string key { get; set; }
    }

    public class Logging
    {
        public bool IncludeScopes { get; set; }
        public LogLevel LogLevel { get; set; }
    }

    public class Tenant
    {
        public object Id { get; set; }
        public string SubscriptionName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string ContactPerson { get; set; }
        public string CreatedOn { get; set; }
        public List<string> Services { get; set; }
        public string Name { get; set; }
        public List<string> Hostnames { get; set; }
        public string Theme { get; set; }
        public string ConnectionString { get; set; }
    }

    public class Multitenancy
    {
        public List<Tenant> Tenants { get; set; }
    }

    public class AppSettings
    {
        public Data Data { get; set; }
        public Logging Logging { get; set; }
        public Multitenancy Multitenancy { get; set; }
    }
}