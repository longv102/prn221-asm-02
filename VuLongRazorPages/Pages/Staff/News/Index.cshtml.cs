using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace VuLongRazorPages.Pages.Staff.News
{
    public class IndexModel : PageModel
    {
        private readonly Repositories.Databases.FunewsManagementDbContext _context;

        public IndexModel(Repositories.Databases.FunewsManagementDbContext context)
        {
            _context = context;
        }

        public IList<NewsArticle> NewsArticle { get;set; } = default!;

        public async Task OnGetAsync()
        {
            NewsArticle = await _context.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy).ToListAsync();
        }
    }
}
