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
    public class IndexModel : PageModel
    {
        private readonly HonorSystem.sakila.ZerodropContext _context;

        public IndexModel(HonorSystem.sakila.ZerodropContext context)
        {
            _context = context;
        }

        public IList<Honorentry> Honorentry { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Honorentries != null)
            {
                Honorentry = await _context.Honorentries
                    .Include(h => h.HonorEntryType)
                    .Include(h => h.Player).ToListAsync();
            }
        }
    }
}
