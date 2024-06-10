using BO.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace VuLongRazorPages.Pages.Staff.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public DeleteModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [BindProperty]
        public CategoryDto Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryService.GetCategory((short)id);

            if (category == null)
            {
                return NotFound();
            }
            else
            {
                Category = category;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _categoryService.DeleteCategory((short)id);
            switch (result)
            {
                case BO.Enums.CategoryOperationResult.Success:
                    return RedirectToPage("./Index");
                case BO.Enums.CategoryOperationResult.Used:
                    ModelState.AddModelError(string.Empty, "Cannot delete this category because it is used in news!");
                    Category = await _categoryService.GetCategory((short)id);
                    break;
            }
            return Page();
        }
    }
}
