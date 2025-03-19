using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HonorSystem.sakila;
using HonorSystem.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HonorSystem.Pages.HonorEntry
{
    public class BulkInsertModel : PageModel
    {
        private readonly ZerodropContext _context;

        [BindProperty]
        public BulkInsertViewModel BulkInsertViewModel { get; set; } = new BulkInsertViewModel();

        public BulkInsertModel(HonorSystem.sakila.ZerodropContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["HonorEntryTypeId"] = new SelectList(_context.Honorentrytypes, "IdHonorEntryType", "Type");
            ViewData["PlayerId"] = new MultiSelectList(_context.Members, "IdMembers", "Name");
            ViewData["HonorEntryTypes"] = _context.Honorentrytypes.ToList();
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Repopulate dropdowns on failure
                var HonorEntryTypeSelectList = new SelectList(await _context.Honorentrytypes.ToListAsync(), "IdHonorEntryType", "IdHonorEntryType");
                var PlayerSelectList = new MultiSelectList(await _context.Members.ToListAsync(), "IdMembers", "Name");
                return Page();
            }

            // Add valid entries to the database
            foreach (var entry in BulkInsertViewModel.PlayerId)
            {
                var temp = new Honorentry() {
                    HonorEntryType = BulkInsertViewModel.HonorEntryType,
                    AssignedPoints = BulkInsertViewModel.AssignedPoints,
                    Description = BulkInsertViewModel.Description,
                    EntryDate = BulkInsertViewModel.EntryDate,
                    ExpirationDate = BulkInsertViewModel.ExpirationDate,
                    HonorEntryTypeId = BulkInsertViewModel.HonorEntryTypeId,
                    Leftiteminguildstorages = BulkInsertViewModel.Leftiteminguildstorages,
                    PlayerId = entry,
                    Player = BulkInsertViewModel.Players.FirstOrDefault( x => x.IdMembers == entry),
                };

                _context.Honorentries.Add(temp);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index"); // Redirect to the index page
        }
    }

}
