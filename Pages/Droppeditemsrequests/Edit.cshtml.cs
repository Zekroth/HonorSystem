using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HonorSystem.sakila;

namespace HonorSystem.Pages.DroppedItemsRequests
{
    public class EditModel : PageModel
    {
        private readonly HonorSystem.sakila.ZerodropContext _context;

        public EditModel(HonorSystem.sakila.ZerodropContext context)
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

            var droppeditemrequest = await _context.Droppeditemsrequests
                .Include(x => x.IdLeftItemInGuildStorageNavigation)
                .ThenInclude(x => x.IdItemNavigation)  // Include anche la navigazione verso 'IdItemNavigation' se necessario
                .FirstOrDefaultAsync(m => m.IdDroppedItemsRequests == id);

            if (droppeditemrequest == null)
            {
                return NotFound();
            }

            if (droppeditemrequest.IdLeftItemInGuildStorageNavigation == null)
            {
                return NotFound();
            }

            Droppeditemsrequest = droppeditemrequest;

            ViewData["IdItem"] = new SelectList(_context.Items, "IdItem", "IdItem", droppeditemrequest.IdLeftItemInGuildStorageNavigation.IdItem);
            ViewData["PlayerId"] = new SelectList(_context.Members, "IdMembers", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Droppeditemsrequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemrequestExists(Droppeditemsrequest.IdDroppedItemsRequests))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ItemrequestExists(int id)
        {
            return (_context.Droppeditemsrequests?.Any(e => e.IdDroppedItemsRequests == id)).GetValueOrDefault();
        }
    }
}
