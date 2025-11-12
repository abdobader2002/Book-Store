using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Book_Store_MVC_Project.Filters
{
    public class HandleErrorAttribute : Attribute,IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            ContentResult contentResult = new ContentResult();
            contentResult.Content = "Exception Happened";
            context.Result = contentResult;
        }
    }
}
