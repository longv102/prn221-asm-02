using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VuLongRazorPages.Pages.Staff
{
    public class StaffRedirectModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StaffRedirectModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? StaffName { get; private set; }

        public string? StaffEmail { get; set; }

        public ActionResult OnGet()
        {
            // Retrieve the name of current logged in staff
            var staffName = _httpContextAccessor.HttpContext?.Session?.GetString("StaffName") ?? string.Empty;
            StaffName = staffName;

            // Retrieve the email of current logged in staff
            var staffEmail = _httpContextAccessor.HttpContext?.Session?.GetString("StaffName") ?? string.Empty;
            StaffEmail = staffEmail;

            var role = _httpContextAccessor.HttpContext?.Session?.GetString("Role");
            if (string.IsNullOrEmpty(role) || "Staff" != role)
            {
                return RedirectToPage("../Index");
            }
            return Page();
        }
    }
}
