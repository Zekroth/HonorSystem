using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HonorSystem.sakila;

namespace HonorSystem.Pages.HonorEntryTypes
{
    public class EditModel : PageModel
    {
        private readonly HonorSystem.sakila.EvildogsContext _context;

        public EditModel(HonorSystem.sakila.EvildogsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Honorentrytype Honorentrytype { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Honorentrytypes == null)
            {
                return NotFound();
            }

            var honorentrytype =  await _context.Honorentrytypes.FirstOrDefaultAsync(m => m.IdHonorEntryType == id);
            if (honorentrytype == null)
            {
                return NotFound();
            }
            Honorentrytype = honorentrytype;
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

            _context.Attach(Honorentrytype).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HonorentrytypeExists(Honorentrytype.IdHonorEntryType))
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

        private bool HonorentrytypeExists(int id)
        {
          return (_context.Honorentrytypes?.Any(e => e.IdHonorEntryType == id)).GetValueOrDefault();
        }
    }
}
