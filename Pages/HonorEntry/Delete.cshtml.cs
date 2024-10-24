using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HonorSystem.sakila;

namespace HonorSystem.Pages.HonorEntry
{
    public class DeleteModel : PageModel
    {
        private readonly HonorSystem.sakila.EvildogsContext _context;

        public DeleteModel(HonorSystem.sakila.EvildogsContext context)
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

            var honorentry = await _context.Honorentries.FirstOrDefaultAsync(m => m.IdHonorEntry == id);

            if (honorentry == null)
            {
                return NotFound();
            }
            else 
            {
                Honorentry = honorentry;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Honorentries == null)
            {
                return NotFound();
            }
            var honorentry = await _context.Honorentries.FindAsync(id);

            if (honorentry != null)
            {
                Honorentry = honorentry;
                _context.Honorentries.Remove(Honorentry);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
