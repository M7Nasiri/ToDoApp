using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace App.EndPoints.MVC.ToDo.Filters
{
    public class RedirectIfAuthenticatedFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.User;

            if (user.Identity != null && user.Identity.IsAuthenticated)
            {
                var ReturnUrl = context.HttpContext.Request.Query["ReturnUrl"].ToString();

                if (string.IsNullOrEmpty(ReturnUrl))
                {
                    context.Result = new RedirectToActionResult("Index", "User", null);
                }
                else
                {
                    context.Result = new RedirectResult(ReturnUrl);
                }
            }
            base.OnActionExecuting(context);
        }
    }
    //public override void OnActionExecuting(ActionExecutingContext context)
    //{
    //    var user = context.HttpContext.User;

    //    if (user.Identity != null && user.Identity.IsAuthenticated)
    //    {
    //        context.Result = new RedirectToActionResult("Index", "User", null);
    //    }

    //    base.OnActionExecuting(context);
    //}
}
