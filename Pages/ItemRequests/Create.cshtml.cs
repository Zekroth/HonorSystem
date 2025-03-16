using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HonorSystem.sakila;

namespace HonorSystem.Pages.ItemRequests
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
        ViewData["ItemId"] = new SelectList(_context.Items, "IdItem", "ItemName");
        ViewData["PlayerId"] = new SelectList(_context.Members, "IdMembers", "Name");
            return Page();
        }

        [BindProperty]
        public Itemrequest Itemrequest { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Itemrequests == null || Itemrequest == null)
            {
                return Page();
            }

            _context.Itemrequests.Add(Itemrequest);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
