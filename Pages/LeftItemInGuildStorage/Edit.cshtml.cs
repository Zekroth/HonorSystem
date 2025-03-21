﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HonorSystem.sakila;

namespace HonorSystem.Pages.LeftItemInGuildStorage
{
    public class EditModel : PageModel
    {
        private readonly HonorSystem.sakila.ZerodropContext _context;

        public EditModel(HonorSystem.sakila.ZerodropContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Leftiteminguildstorage Leftiteminguildstorage { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Leftiteminguildstorages == null)
            {
                return NotFound();
            }

            var leftiteminguildstorage =  await _context.Leftiteminguildstorages.FirstOrDefaultAsync(m => m.Id == id);
            if (leftiteminguildstorage == null)
            {
                return NotFound();
            }
            Leftiteminguildstorage = leftiteminguildstorage;
           ViewData["IdHonorEntry"] = new SelectList(_context.Honorentries, "IdHonorEntry", "IdHonorEntry");
           ViewData["IdItem"] = new SelectList(_context.Items, "IdItem", "IdItem");
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

            _context.Attach(Leftiteminguildstorage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeftiteminguildstorageExists(Leftiteminguildstorage.Id))
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

        private bool LeftiteminguildstorageExists(int id)
        {
          return (_context.Leftiteminguildstorages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
