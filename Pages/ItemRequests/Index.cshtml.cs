using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HonorSystem.sakila;

namespace HonorSystem.Pages.ItemRequests
{
    public class IndexModel : PageModel
    {
        private readonly HonorSystem.sakila.ZerodropContext _context;

        public IndexModel(HonorSystem.sakila.ZerodropContext context)
        {
            _context = context;
        }

        public IList<Itemrequest> Itemrequest { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Itemrequests != null)
            {
                Itemrequest = await _context.Itemrequests
                .Include(i => i.Item)
                .Include(i => i.Player).ToListAsync();
            }
        }
    }
}
