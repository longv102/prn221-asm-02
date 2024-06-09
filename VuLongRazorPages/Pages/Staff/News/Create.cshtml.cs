﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositories;
using Repositories.Databases;

namespace VuLongRazorPages.Pages.Staff.News
{
    public class CreateModel : PageModel
    {
        private readonly Repositories.Databases.FunewsManagementDbContext _context;

        public CreateModel(Repositories.Databases.FunewsManagementDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryDesciption");
        ViewData["CreatedById"] = new SelectList(_context.SystemAccounts, "AccountId", "AccountId");
            return Page();
        }

        [BindProperty]
        public NewsArticle NewsArticle { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.NewsArticles.Add(NewsArticle);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
