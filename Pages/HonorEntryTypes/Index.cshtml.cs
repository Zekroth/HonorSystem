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
    public class IndexModel : PageModel
    {
        private readonly HonorSystem.sakila.EvildogsContext _context;

        public IndexModel(HonorSystem.sakila.EvildogsContext context)
        {
            _context = context;
        }

        public IList<Honorentrytype> Honorentrytype { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Honorentrytypes != null)
            {
                Honorentrytype = await _context.Honorentrytypes.ToListAsync();
            }
        }
    }
}
