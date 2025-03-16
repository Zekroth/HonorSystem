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
    public class DetailsModel : PageModel
    {
        private readonly HonorSystem.sakila.ZerodropContext _context;

        public DetailsModel(HonorSystem.sakila.ZerodropContext context)
        {
            _context = context;
        }

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
    }
}
