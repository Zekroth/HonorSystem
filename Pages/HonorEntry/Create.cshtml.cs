using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HonorSystem.sakila;

namespace HonorSystem.Pages.HonorEntry
{
    public class CreateModel : PageModel
    {
        private readonly HonorSystem.sakila.EvildogsContext _context;

        public CreateModel(HonorSystem.sakila.EvildogsContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["HonorEntryTypeId"] = new SelectList(_context.Honorentrytypes, "IdHonorEntryType", "Type");
            ViewData["PlayerId"] = new SelectList(_context.Members, "IdMembers", "Name");
            return Page();
        }

        [BindProperty]
        public Honorentry Honorentry { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Honorentries == null || Honorentry == null)
            {
                return Page();
            }

            _context.Honorentries.Add(Honorentry);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
