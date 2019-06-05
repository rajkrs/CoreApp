using CoreApp.Account.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApp.DataAccess
{
    public class BaseRepository : AuthorizeProfile
    {
        public BaseRepository(AuthorizeProfile authorizeProfile)
        {
            UserId = authorizeProfile.UserId;
            OrgnizationId = authorizeProfile.OrgnizationId;
        }

    }
}
