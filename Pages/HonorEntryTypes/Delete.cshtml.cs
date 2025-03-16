using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HonorSystem.sakila;

namespace HonorSystem.Pages.HonorEntryTypes
{
    public class DeleteModel : PageModel
    {
        private readonly HonorSystem.sakila.ZerodropContext _context;

        public DeleteModel(HonorSystem.sakila.ZerodropContext context)
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

            var honorentrytype = await _context.Honorentrytypes.FirstOrDefaultAsync(m => m.IdHonorEntryType == id);

            if (honorentrytype == null)
            {
                return NotFound();
            }
            else 
            {
                Honorentrytype = honorentrytype;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Honorentrytypes == null)
            {
                return NotFound();
            }
            var honorentrytype = await _context.Honorentrytypes.FindAsync(id);

            if (honorentrytype != null)
            {
                Honorentrytype = honorentrytype;
                _context.Honorentrytypes.Remove(Honorentrytype);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
