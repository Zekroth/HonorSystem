﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HonorSystem.sakila;

namespace HonorSystem.Controllers
{
    public class HonorEntriesController : Controller
    {
        private readonly EvildogsContext _context;

        public HonorEntriesController(EvildogsContext context)
        {
            _context = context;
        }

        // GET: HonorEntries
        public async Task<IActionResult> Index()
        {
            var evildogsContext = _context.Honorentries.Include(h => h.HonorEntryType).Include(h => h.Player);
            return View(await evildogsContext.ToListAsync());
        }

        // GET: HonorEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Honorentries == null)
            {
                return NotFound();
            }

            var honorentry = await _context.Honorentries
                .Include(h => h.HonorEntryType)
                .Include(h => h.Player)
                .FirstOrDefaultAsync(m => m.IdHonorEntry == id);
            if (honorentry == null)
            {
                return NotFound();
            }

            return View(honorentry);
        }

        // GET: HonorEntries/Create
        public IActionResult Create()
        {
            ViewData["HonorEntryTypeId"] = new SelectList(_context.Honorentrytypes, "IdHonorEntryType", "IdHonorEntryType");
            ViewData["PlayerId"] = new SelectList(_context.Members, "IdMembers", "IdMembers");
            return View();
        }

        // POST: HonorEntries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHonorEntry,EntryDate,AssignedPoints,Description,ExpirationDate,HonorEntryTypeId,PlayerId")] Honorentry honorentry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(honorentry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HonorEntryTypeId"] = new SelectList(_context.Honorentrytypes, "IdHonorEntryType", "IdHonorEntryType", honorentry.HonorEntryTypeId);
            ViewData["PlayerId"] = new SelectList(_context.Members, "IdMembers", "IdMembers", honorentry.PlayerId);
            return View(honorentry);
        }

        // GET: HonorEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Honorentries == null)
            {
                return NotFound();
            }

            var honorentry = await _context.Honorentries.FindAsync(id);
            if (honorentry == null)
            {
                return NotFound();
            }
            ViewData["HonorEntryTypeId"] = new SelectList(_context.Honorentrytypes, "IdHonorEntryType", "IdHonorEntryType", honorentry.HonorEntryTypeId);
            ViewData["PlayerId"] = new SelectList(_context.Members, "IdMembers", "IdMembers", honorentry.PlayerId);
            return View(honorentry);
        }

        // POST: HonorEntries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHonorEntry,EntryDate,AssignedPoints,Description,ExpirationDate,HonorEntryTypeId,PlayerId")] Honorentry honorentry)
        {
            if (id != honorentry.IdHonorEntry)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(honorentry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HonorentryExists(honorentry.IdHonorEntry))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["HonorEntryTypeId"] = new SelectList(_context.Honorentrytypes, "IdHonorEntryType", "IdHonorEntryType", honorentry.HonorEntryTypeId);
            ViewData["PlayerId"] = new SelectList(_context.Members, "IdMembers", "IdMembers", honorentry.PlayerId);
            return View(honorentry);
        }

        // GET: HonorEntries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Honorentries == null)
            {
                return NotFound();
            }

            var honorentry = await _context.Honorentries
                .Include(h => h.HonorEntryType)
                .Include(h => h.Player)
                .FirstOrDefaultAsync(m => m.IdHonorEntry == id);
            if (honorentry == null)
            {
                return NotFound();
            }

            return View(honorentry);
        }

        // POST: HonorEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Honorentries == null)
            {
                return Problem("Entity set 'EvildogsContext.Honorentries'  is null.");
            }
            var honorentry = await _context.Honorentries.FindAsync(id);
            if (honorentry != null)
            {
                _context.Honorentries.Remove(honorentry);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HonorentryExists(int id)
        {
          return (_context.Honorentries?.Any(e => e.IdHonorEntry == id)).GetValueOrDefault();
        }
    }
}