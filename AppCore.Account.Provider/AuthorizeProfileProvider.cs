using CoreApp.Account.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreApp.Account.Provider
{
     public class AuthorizeProfileProvider
        {
            private IHttpContextAccessor _accessor;
            public AuthorizeProfileProvider(IHttpContextAccessor accessor)
            {
                _accessor = accessor;
            }

            public AuthorizeProfile GetProfile()
            {
                var context = _accessor.HttpContext;

                AuthorizeProfile authorizeProfile = null;

                var claims = context.User.Claims;
                var userIdData = claims.FirstOrDefault(c => c.Type == "UserId");
                var userOrgnizarionData = claims.FirstOrDefault(c => c.Type == "Organization");


                int UserId = -1;
                int OrgnizationId = -1;

                int.TryParse(userIdData?.Value, out UserId);
                int.TryParse(userOrgnizarionData?.Value, out OrgnizationId);

                if (UserId != -1 && OrgnizationId != -1)
                {
                    authorizeProfile = new AuthorizeProfile();
                    authorizeProfile.UserId = UserId;
                    authorizeProfile.UserId = OrgnizationId;
                }

                return authorizeProfile;

            }
        }

}
