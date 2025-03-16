using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HonorSystem.sakila;

namespace HonorSystem.Pages.LeftItemInGuildStorage
{
    public class CreateModel : PageModel
    {
        private readonly HonorSystem.sakila.ZerodropContext _context;

        public CreateModel(HonorSystem.sakila.ZerodropContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["IdHonorEntry"] = new SelectList(_context.Honorentries, "IdHonorEntry", "EntryDate");
        ViewData["IdItem"] = new SelectList(_context.Items, "IdItem", "ItemName");
            return Page();
        }

        [BindProperty]
        public Leftiteminguildstorage Leftiteminguildstorage { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Leftiteminguildstorages == null || Leftiteminguildstorage == null)
            {
                return Page();
            }

            _context.Leftiteminguildstorages.Add(Leftiteminguildstorage);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
