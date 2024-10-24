using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HonorSystem.sakila;

namespace HonorSystem.Pages.Bosses
{
    public class EditModel : PageModel
    {
        private readonly HonorSystem.sakila.EvildogsContext _context;

        public EditModel(HonorSystem.sakila.EvildogsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Boss Boss { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Bosses == null)
            {
                return NotFound();
            }

            var boss =  await _context.Bosses.FirstOrDefaultAsync(m => m.IdBoss == id);
            if (boss == null)
            {
                return NotFound();
            }
            Boss = boss;
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

            _context.Attach(Boss).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BossExists(Boss.IdBoss))
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

        private bool BossExists(int id)
        {
          return (_context.Bosses?.Any(e => e.IdBoss == id)).GetValueOrDefault();
        }
    }
}
