using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using LoginForm.ViewModels;
using System;

namespace LoginForm.ActionFilters
{
    public class Log : ActionFilterAttribute , IResultFilter
    {
        private string ViewBagMessage;
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            LogOutPut("OnActionExecuted", context.RouteData);
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            LogOutPut("OnActionExecuting", context.RouteData);
            var Argument = context.ActionArguments;

            if (Argument.Count > 1)
            {
                context.Result = new BadRequestResult();
                return;
            }

            var Comment = Argument.First().Value as CommentViewModel;
            if (String.IsNullOrWhiteSpace(Comment.Title) || Comment.Title.Length < 2 || Comment.Title.Length > 50)
            {
                ViewBagMessage = "Titel is Invalid";
                context.Result = new ViewResult
                {
                    ViewName = @"~/Views/Home/Comment.cshtml",
                    StatusCode = 200,
                };
                return;
            }

            if (String.IsNullOrWhiteSpace(Comment.Text) || Comment.Text.Length < 3 || Comment.Text.Length < 500)
            {
                ViewBagMessage = "Text Comment is invalid";
                context.Result = new ViewResult
                {
                    ViewName = @"~/Views/Home/Comment.cshtml",
                    StatusCode = 200,
                };
                return;
            }
        }
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            LogOutPut("OnResultExecuted", context.RouteData);
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            LogOutPut("OnResultExecuting",context.RouteData);
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await base.OnActionExecutionAsync(context, next);
            var Test = context.Controller as Controller;
            Test.ViewBag.Message = ViewBagMessage;
        }
        private void LogOutPut(string MethodName,RouteData routeData)
        {
            var ControllerName = routeData.Values["controller"];
            var ActionName = routeData.Values["action"];
            var Message = $"Controller : {ControllerName} , Action : {ActionName} on {MethodName}";

            Debug.WriteLine(Message);
        }
    }
}
