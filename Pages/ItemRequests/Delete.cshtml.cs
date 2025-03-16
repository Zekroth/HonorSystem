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
    public class DeleteModel : PageModel
    {
        private readonly HonorSystem.sakila.ZerodropContext _context;

        public DeleteModel(HonorSystem.sakila.ZerodropContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Itemrequest Itemrequest { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Itemrequests == null)
            {
                return NotFound();
            }

            var itemrequest = await _context.Itemrequests.FirstOrDefaultAsync(m => m.IdItemRequest == id);

            if (itemrequest == null)
            {
                return NotFound();
            }
            else 
            {
                Itemrequest = itemrequest;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Itemrequests == null)
            {
                return NotFound();
            }
            var itemrequest = await _context.Itemrequests.FindAsync(id);

            if (itemrequest != null)
            {
                Itemrequest = itemrequest;
                _context.Itemrequests.Remove(Itemrequest);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
