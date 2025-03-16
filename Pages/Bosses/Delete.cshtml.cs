using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HonorSystem.sakila;

namespace HonorSystem.Pages.Bosses
{
    public class DeleteModel : PageModel
    {
        private readonly HonorSystem.sakila.ZerodropContext _context;

        public DeleteModel(HonorSystem.sakila.ZerodropContext context)
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

            var boss = await _context.Bosses.FirstOrDefaultAsync(m => m.IdBoss == id);

            if (boss == null)
            {
                return NotFound();
            }
            else 
            {
                Boss = boss;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Bosses == null)
            {
                return NotFound();
            }
            var boss = await _context.Bosses.FindAsync(id);

            if (boss != null)
            {
                Boss = boss;
                _context.Bosses.Remove(Boss);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
