using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace GyanDyan.Exceptions
{
    public class GeneralExceptionHandlerFilter : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if(context.Exception is DuplicateUserException)
            {
                Console.WriteLine($"LOG {context.Exception.Message}");
                context.Result = new ObjectResult(new { Message = "User already exists" })
                {
                    StatusCode = 400
                };
                context.ExceptionHandled = true;
            }

            if (context.Exception is LoginFailedException)
            {
                Console.WriteLine($"LOG: {context.Exception.Message}");

                context.Result = new ObjectResult(new { Message = "Invalid Credentials" })
                {
                    StatusCode = 401
                };
                context.ExceptionHandled = true;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
