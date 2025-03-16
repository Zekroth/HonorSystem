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
    public class DeleteModel : PageModel
    {
        private readonly HonorSystem.sakila.ZerodropContext _context;

        public DeleteModel(HonorSystem.sakila.ZerodropContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Droppeditemsrequest Droppeditemsrequest { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Droppeditemsrequests == null)
            {
                return NotFound();
            }

            var droppeditemsrequest = await _context.Droppeditemsrequests.FirstOrDefaultAsync(m => m.IdDroppedItemsRequests == id);

            if (droppeditemsrequest == null)
            {
                return NotFound();
            }
            else 
            {
                Droppeditemsrequest = droppeditemsrequest;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Droppeditemsrequests == null)
            {
                return NotFound();
            }
            var droppeditemsrequest = await _context.Droppeditemsrequests.FindAsync(id);

            if (droppeditemsrequest != null)
            {
                Droppeditemsrequest = droppeditemsrequest;
                _context.Droppeditemsrequests.Remove(Droppeditemsrequest);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
