
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CoreApp.WebApi.Resources;

namespace CoreApp.WebApi.Models
{
    public class UserLogin
    {
        [DisplayName("LoginId")]
        public int UserId { get; set; }

        [Required(ErrorMessageResourceName = "Password", ErrorMessageResourceType = typeof(ValidationMessage)), MinLength(4), MaxLength(10)]
        public string Password { get; set; }
        public DateTime? RequestTime { get; set; }

    }
}
