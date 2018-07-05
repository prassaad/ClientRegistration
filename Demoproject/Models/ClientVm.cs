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
}