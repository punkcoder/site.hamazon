using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using Umbraco.Web.WebApi;

namespace Hamazon.Controllers
{
    public class LoginRequest
    {
        public string email { get; set; }
        public string password { get; set; }
    }

    public class UserController : UmbracoApiController
    {
        [System.Web.Mvc.HttpPost]
        public bool LoginUser([FromBody] LoginRequest request)
        {
            try
            {
                var memberservices = Services.MemberService;
                int emailcount = 0;
                var members = memberservices.FindByUsername(request.email, 0, 10, out emailcount);
                if (members.Any())
                {
                    if (Membership.ValidateUser(request.email, request.password))
                    {
                        FormsAuthentication.SetAuthCookie(request.email, true);
                    }
                }
            }
            catch (Exception Ex)
            {
                return false;
            }
            return true;
        }
    }
}