using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HonorSystem.sakila;

namespace HonorSystem.Pages.ItemRequests
{
    public class EditModel : PageModel
    {
        private readonly HonorSystem.sakila.EvildogsContext _context;

        public EditModel(HonorSystem.sakila.EvildogsContext context)
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

            var itemrequest =  await _context.Itemrequests.FirstOrDefaultAsync(m => m.IdItemRequest == id);
            if (itemrequest == null)
            {
                return NotFound();
            }
            Itemrequest = itemrequest;
           ViewData["ItemId"] = new SelectList(_context.Items, "IdItem", "IdItem");
           ViewData["PlayerId"] = new SelectList(_context.Members, "IdMembers", "IdMembers");
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

            _context.Attach(Itemrequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemrequestExists(Itemrequest.IdItemRequest))
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
          return (_context.Itemrequests?.Any(e => e.IdItemRequest == id)).GetValueOrDefault();
        }
    }
}
