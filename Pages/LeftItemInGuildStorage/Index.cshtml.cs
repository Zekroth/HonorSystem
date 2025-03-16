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
    public class IndexModel : PageModel
    {
        private readonly HonorSystem.sakila.ZerodropContext _context;

        public IndexModel(HonorSystem.sakila.ZerodropContext context)
        {
            _context = context;
        }

        public IList<Leftiteminguildstorage> Leftiteminguildstorage { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Leftiteminguildstorages != null)
            {
                Leftiteminguildstorage = await _context.Leftiteminguildstorages
                    .Include(l => l.IdHonorEntryNavigation)
                    .Include(l => l.IdItemNavigation)
                    .ToListAsync();
            }
        }
    }
}
