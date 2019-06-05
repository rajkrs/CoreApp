using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApp.Account.Model
{
    public class AuthorizeProfile
    {
        public AuthorizeProfile()
        {

        }
        public AuthorizeProfile(AuthorizeProfile authorizeProfile)
        {
            UserId = authorizeProfile.UserId;
            OrgnizationId = authorizeProfile.OrgnizationId;
        }


        public Int64 UserId { get; set; }

        public Int64 OrgnizationId { get; set; }
    }
}
