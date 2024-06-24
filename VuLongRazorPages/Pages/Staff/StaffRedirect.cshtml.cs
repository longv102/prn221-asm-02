using Microsoft.AspNetCore.Mvc;

namespace VuLongRazorPages.Pages.Staff
{
    public class StaffRedirectModel : BasePageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StaffRedirectModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? StaffName { get; private set; }

        public string? StaffEmail { get; set; }

        protected override string RequiredRole => "Staff";

        public ActionResult OnGet()
        {
            // Retrieve the name of current logged in staff
            var staffName = _httpContextAccessor.HttpContext?.Session?.GetString("StaffName") ?? string.Empty;
            StaffName = staffName;

            // Retrieve the email of current logged in staff
            var staffEmail = _httpContextAccessor.HttpContext?.Session?.GetString("StaffName") ?? string.Empty;
            StaffEmail = staffEmail;
            return Page();
        }
    }
}
