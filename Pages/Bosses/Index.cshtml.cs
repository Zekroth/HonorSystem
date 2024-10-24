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
    public class IndexModel : PageModel
    {
        private readonly HonorSystem.sakila.EvildogsContext _context;

        public IndexModel(HonorSystem.sakila.EvildogsContext context)
        {
            _context = context;
        }

        public IList<Boss> Boss { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Bosses != null)
            {
                Boss = await _context.Bosses.ToListAsync();
            }
        }
    }
}
