using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
        public HttpResponseMessage LoginUser([FromBody] LoginRequest request)
        {
            var resp = new HttpResponseMessage();
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

                        var payload = new Dictionary<string, object>()
                        {
                            { "UserId", members.First().Id},
                            { "email", request.email },
                            { "password", request.password }
                        };
                        var secretKey = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";
                        string token = JWT.JsonWebToken.Encode(payload, secretKey, JWT.JwtHashAlgorithm.HS256);

                        resp.Content = new StringContent(token);
                    }
                    else
                    {
                        resp.Content = new StringContent("false:LoginIncorrect");
                    }
                }
                else
                {
                    resp.Content = new StringContent("false:NotFound");
                }
            }
            catch (Exception Ex)
            {
                resp.Content = new StringContent("false:AllHellBrokeLoose"); 
            }
            return resp;
        }

        [System.Web.Mvc.HttpPost]
        public HttpResponse LogoutUser()
        {
            return null;
        }
    }
}