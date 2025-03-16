using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HonorSystem.sakila;

namespace HonorSystem.Pages.HonorEntryTypes
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
            return Page();
        }

        [BindProperty]
        public Honorentrytype Honorentrytype { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Honorentrytypes == null || Honorentrytype == null)
            {
                return Page();
            }

            _context.Honorentrytypes.Add(Honorentrytype);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
