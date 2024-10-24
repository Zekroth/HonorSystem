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
    public class DetailsModel : PageModel
    {
        private readonly HonorSystem.sakila.EvildogsContext _context;

        public DetailsModel(HonorSystem.sakila.EvildogsContext context)
        {
            _context = context;
        }

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
    }
}
