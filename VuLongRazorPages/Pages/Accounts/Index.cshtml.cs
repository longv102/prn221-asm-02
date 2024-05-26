using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Database;

namespace VuLongRazorPages.Pages.Accounts
{
    public class IndexModel : PageModel
    {
        private readonly DAL.Database.FunewsManagementDbContext _context;

        public IndexModel(DAL.Database.FunewsManagementDbContext context)
        {
            _context = context;
        }

        public IList<SystemAccount> SystemAccount { get;set; } = default!;

        public async Task OnGetAsync()
        {
            SystemAccount = await _context.SystemAccounts.ToListAsync();
        }
    }
}
