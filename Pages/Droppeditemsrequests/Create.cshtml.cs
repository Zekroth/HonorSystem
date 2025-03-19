using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HonorSystem.sakila;
using Microsoft.EntityFrameworkCore;

namespace HonorSystem.Pages.DroppedItemsRequests
{
    public class CreateModel : PageModel
    {
        private readonly HonorSystem.sakila.ZerodropContext _context;

        public CreateModel(HonorSystem.sakila.ZerodropContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["IdMembers"] = new SelectList(_context.Members, "IdMembers", "Name");
            ViewData["IdLeftItemInGuildStorage"] = new SelectList(_context.Leftiteminguildstorages
                .Include(x => x.IdItemNavigation), "IdLeftItemInGuildStorage", "IdItemNavigation");
            return Page();
        }

        [BindProperty]
        public Droppeditemsrequest DroppedItemRequest { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Droppeditemsrequests == null || DroppedItemRequest == null)
            {
                return Page();
            }

            _context.Droppeditemsrequests.Add(DroppedItemRequest);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
