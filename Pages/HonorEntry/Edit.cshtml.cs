using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HonorSystem.sakila;

namespace HonorSystem.Pages.HonorEntry
{
    public class EditModel : PageModel
    {
        private readonly HonorSystem.sakila.EvildogsContext _context;

        public EditModel(HonorSystem.sakila.EvildogsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Honorentry Honorentry { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Honorentries == null)
            {
                return NotFound();
            }

            var honorentry =  await _context.Honorentries.FirstOrDefaultAsync(m => m.IdHonorEntry == id);
            if (honorentry == null)
            {
                return NotFound();
            }
            Honorentry = honorentry;
           ViewData["HonorEntryTypeId"] = new SelectList(_context.Honorentrytypes, "IdHonorEntryType", "Type");
           ViewData["PlayerId"] = new SelectList(_context.Members, "IdMembers", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Honorentry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HonorentryExists(Honorentry.IdHonorEntry))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool HonorentryExists(int id)
        {
          return (_context.Honorentries?.Any(e => e.IdHonorEntry == id)).GetValueOrDefault();
        }
    }
}
