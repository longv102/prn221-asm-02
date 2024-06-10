using BO.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Contracts;
using Services.Interfaces;

namespace VuLongRazorPages.Pages.Staff.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateModel(ICategoryService categoryService , IHttpContextAccessor httpContextAccessor)
        {
            _categoryService = categoryService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult OnGet()
        {
            var role = _httpContextAccessor.HttpContext?.Session?.GetString("Role");
            if (string.IsNullOrEmpty(role) || "Staff" != role)
                return RedirectToPage("../StaffRedirect");

            return Page();
        }

        [BindProperty]
        public CategoryDto Category { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _categoryService.CreateCategory(Category);
            switch (result)
            {
                case BO.Enums.CategoryOperationResult.Success:
                    return RedirectToPage("./Index");
                case BO.Enums.CategoryOperationResult.Empty:
                    ModelState.AddModelError(string.Empty, "Category is empty!");
                    break;
            }
            return Page();
        }
    }
}
