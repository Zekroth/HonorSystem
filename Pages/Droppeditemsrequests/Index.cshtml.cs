using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HonorSystem.sakila;

namespace HonorSystem.Pages.Droppeditemsrequests
{
    public class IndexModel : PageModel
    {
        private readonly HonorSystem.sakila.ZerodropContext _context;

        public IndexModel(HonorSystem.sakila.ZerodropContext context)
        {
            _context = context;
        }

        public IList<Droppeditemsrequest> DIRequest { get;set; }
        public IList<HonorSystem.sakila.Classifica> Classificas { get;set; } = default!;


        public async Task OnGetAsync()
        {
            DIRequest = await _context.Droppeditemsrequests
                .Include(d => d.IdLeftItemInGuildStorageNavigation)
                .Include(d => d.IdMemberNavigation)
                .Include(d => d.IdLeftItemInGuildStorageNavigation.IdItemNavigation)
                .ToListAsync();

                if (_context.Classificas != null)
            {
                Classificas = await _context.Classificas.ToListAsync();
            }

            if (_context.Classificas != null)
            {
                Classificas = await _context.Classificas.ToListAsync();
            }
        }
    }
}
