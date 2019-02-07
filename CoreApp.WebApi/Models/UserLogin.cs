using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.WebApi.Models
{
    public class UserLogin
    {
        [DisplayName("LoginId")]
        public int UserId { get; set; }
        public string Password { get; set; }

        public DateTime RequestTime { get; set; }

    }
}
