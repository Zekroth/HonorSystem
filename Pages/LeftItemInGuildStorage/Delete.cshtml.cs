using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HonorSystem.sakila;

namespace HonorSystem.Pages.LeftItemInGuildStorage
{
    public class DeleteModel : PageModel
    {
        private readonly HonorSystem.sakila.EvildogsContext _context;

        public DeleteModel(HonorSystem.sakila.EvildogsContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Leftiteminguildstorage Leftiteminguildstorage { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Leftiteminguildstorages == null)
            {
                return NotFound();
            }

            var leftiteminguildstorage = await _context.Leftiteminguildstorages.FirstOrDefaultAsync(m => m.Id == id);

            if (leftiteminguildstorage == null)
            {
                return NotFound();
            }
            else 
            {
                Leftiteminguildstorage = leftiteminguildstorage;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Leftiteminguildstorages == null)
            {
                return NotFound();
            }
            var leftiteminguildstorage = await _context.Leftiteminguildstorages.FindAsync(id);

            if (leftiteminguildstorage != null)
            {
                Leftiteminguildstorage = leftiteminguildstorage;
                _context.Leftiteminguildstorages.Remove(Leftiteminguildstorage);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
