using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HonorSystem.sakila;

namespace HonorSystem.Pages.Bosses
{
    public class CreateModel : PageModel
    {
        private readonly HonorSystem.sakila.EvildogsContext _context;

        public CreateModel(HonorSystem.sakila.EvildogsContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Boss Boss { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Bosses == null || Boss == null)
            {
                return Page();
            }

            _context.Bosses.Add(Boss);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
