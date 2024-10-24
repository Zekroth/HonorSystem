using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HonorSystem.sakila;

namespace HonorSystem.Controllers
{
    public class LeftItemInGuildStorageController : Controller
    {
        private readonly EvildogsContext _context;

        public LeftItemInGuildStorageController(EvildogsContext context)
        {
            _context = context;
        }

        // GET: LeftItemInGuildStorage
        public async Task<IActionResult> Index()
        {
            var evildogsContext = _context.Leftiteminguildstorages.Include(l => l.IdHonorEntryNavigation).Include(l => l.IdItemNavigation);
            return View(await evildogsContext.ToListAsync());
        }

        // GET: LeftItemInGuildStorage/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Leftiteminguildstorages == null)
            {
                return NotFound();
            }

            var leftiteminguildstorage = await _context.Leftiteminguildstorages
                .Include(l => l.IdHonorEntryNavigation)
                .Include(l => l.IdItemNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leftiteminguildstorage == null)
            {
                return NotFound();
            }

            return View(leftiteminguildstorage);
        }

        // GET: LeftItemInGuildStorage/Create
        public IActionResult Create()
        {
            ViewData["IdHonorEntry"] = new SelectList(_context.Honorentries, "IdHonorEntry", "IdHonorEntry");
            ViewData["IdItem"] = new SelectList(_context.Items, "IdItem", "IdItem");
            return View();
        }

        // POST: LeftItemInGuildStorage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DropDate,CurrentPriceInLucent,IdItem,IdHonorEntry")] Leftiteminguildstorage leftiteminguildstorage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leftiteminguildstorage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdHonorEntry"] = new SelectList(_context.Honorentries, "IdHonorEntry", "IdHonorEntry", leftiteminguildstorage.IdHonorEntry);
            ViewData["IdItem"] = new SelectList(_context.Items, "IdItem", "IdItem", leftiteminguildstorage.IdItem);
            return View(leftiteminguildstorage);
        }

        // GET: LeftItemInGuildStorage/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Leftiteminguildstorages == null)
            {
                return NotFound();
            }

            var leftiteminguildstorage = await _context.Leftiteminguildstorages.FindAsync(id);
            if (leftiteminguildstorage == null)
            {
                return NotFound();
            }
            ViewData["IdHonorEntry"] = new SelectList(_context.Honorentries, "IdHonorEntry", "IdHonorEntry", leftiteminguildstorage.IdHonorEntry);
            ViewData["IdItem"] = new SelectList(_context.Items, "IdItem", "IdItem", leftiteminguildstorage.IdItem);
            return View(leftiteminguildstorage);
        }

        // POST: LeftItemInGuildStorage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DropDate,CurrentPriceInLucent,IdItem,IdHonorEntry")] Leftiteminguildstorage leftiteminguildstorage)
        {
            if (id != leftiteminguildstorage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leftiteminguildstorage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeftiteminguildstorageExists(leftiteminguildstorage.Id))
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
            ViewData["IdHonorEntry"] = new SelectList(_context.Honorentries, "IdHonorEntry", "IdHonorEntry", leftiteminguildstorage.IdHonorEntry);
            ViewData["IdItem"] = new SelectList(_context.Items, "IdItem", "IdItem", leftiteminguildstorage.IdItem);
            return View(leftiteminguildstorage);
        }

        // GET: LeftItemInGuildStorage/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Leftiteminguildstorages == null)
            {
                return NotFound();
            }

            var leftiteminguildstorage = await _context.Leftiteminguildstorages
                .Include(l => l.IdHonorEntryNavigation)
                .Include(l => l.IdItemNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leftiteminguildstorage == null)
            {
                return NotFound();
            }

            return View(leftiteminguildstorage);
        }

        // POST: LeftItemInGuildStorage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Leftiteminguildstorages == null)
            {
                return Problem("Entity set 'EvildogsContext.Leftiteminguildstorages'  is null.");
            }
            var leftiteminguildstorage = await _context.Leftiteminguildstorages.FindAsync(id);
            if (leftiteminguildstorage != null)
            {
                _context.Leftiteminguildstorages.Remove(leftiteminguildstorage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeftiteminguildstorageExists(int id)
        {
          return (_context.Leftiteminguildstorages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
