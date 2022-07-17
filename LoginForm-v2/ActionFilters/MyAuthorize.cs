using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginForm.ActionFilters
{
    public class MyAuthorize : Attribute, IAuthorizationFilter
    {
        private readonly string _policy;

        public MyAuthorize(string Policy)
        {
            _policy = Policy;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new ViewResult()
                {
                    ViewName = "~/Views/Home/Index.cshtml",
                };

                return;
            }

            if (context.HttpContext.User.FindFirst(_policy) == null)
            {
                context.Result = new StatusCodeResult(403);

                return;
            }
        }
    }
}
