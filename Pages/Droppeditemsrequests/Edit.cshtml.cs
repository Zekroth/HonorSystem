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
            if (id == null || id == 0)
                return NotFound();

            Droppeditemsrequest = _context.Droppeditemsrequests
                .Include(x => x.IdMemberNavigation)
                .Include(x => x.IdLeftItemInGuildStorageNavigation)
                .Where(x => x.IdDroppedItemsRequests == id)
                .FirstOrDefault() ?? new();

            Droppeditemsrequest.IdDroppedItemsRequests = id ?? 0;

            var memberid = _context.Members.ToList();  
            ViewData["MemberId"] = new SelectList(memberid, "IdMembers", "Name"); ;
            
            var itemid = _context.Leftiteminguildstorages
                .Include(x => x.IdItemNavigation)
                .Where(x => x.DistributedTo == null)
                .ToList();
            ViewData["ItemId"] = new SelectList(itemid, "Id", "DropDate");

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

            _context.Entry(Droppeditemsrequest).State = EntityState.Modified;

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
