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
    public class DetailsModel : PageModel
    {
        private readonly HonorSystem.sakila.ZerodropContext _context;

        public DetailsModel(HonorSystem.sakila.ZerodropContext context)
        {
            _context = context;
        }

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
    }
}
