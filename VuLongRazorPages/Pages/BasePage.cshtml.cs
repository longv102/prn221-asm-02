using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VuLongRazorPages.Pages
{
    public abstract class BasePageModel : PageModel
    {
        protected abstract string RequiredRole { get; }

        public string Role => HttpContext.Session.GetString("Role") ?? string.Empty;

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            if (string.IsNullOrEmpty(RequiredRole) || Role != RequiredRole)
            {
                context.Result = new RedirectToPageResult("/Index");
            }
            base.OnPageHandlerExecuting(context);
        }
    }
}
