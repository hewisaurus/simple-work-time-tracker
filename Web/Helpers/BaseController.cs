using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SimpleWorkTimeTracker.Helpers
{
    public class BaseController : Controller
    {
        protected static UserAuthorisationInfo UAuthInfo;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    var sidClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid);
                    if (UAuthInfo == null)
                    {
                        UAuthInfo = new UserAuthorisationInfo();
                    }
                    if (sidClaim != null)
                    {
                        UAuthInfo.UserId = Int32.Parse(sidClaim.Value);
                    }
                }
                catch (Exception e)
                {
                    if (UAuthInfo == null)
                    {
                        UAuthInfo = new UserAuthorisationInfo();
                    }
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
