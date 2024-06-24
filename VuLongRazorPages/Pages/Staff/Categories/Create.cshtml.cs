using BO.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Contracts;
using Services.Interfaces;

namespace VuLongRazorPages.Pages.Staff.Categories
{
    public class CreateModel : BasePageModel
    {
        private readonly ICategoryService _categoryService;
        
        public CreateModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public void OnGet()
        {
        }

        [BindProperty]
        public CategoryDto Category { get; set; } = default!;

        protected override string RequiredRole => "Staff";

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
