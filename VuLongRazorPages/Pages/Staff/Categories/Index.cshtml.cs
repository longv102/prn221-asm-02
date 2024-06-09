using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace VuLongRazorPages.Pages.Staff.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Repositories.Databases.FunewsManagementDbContext _context;

        public IndexModel(Repositories.Databases.FunewsManagementDbContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Category = await _context.Categories.ToListAsync();
        }
    }
}
