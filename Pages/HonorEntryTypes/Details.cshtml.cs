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
    public class DetailsModel : PageModel
    {
        private readonly HonorSystem.sakila.EvildogsContext _context;

        public DetailsModel(HonorSystem.sakila.EvildogsContext context)
        {
            _context = context;
        }

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
    }
}
