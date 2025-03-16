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
    public class HonorEntryTypesController : Controller
    {
        private readonly ZerodropContext _context;

        public HonorEntryTypesController(ZerodropContext context)
        {
            _context = context;
        }

        // GET: HonorEntryTypes
        public async Task<IActionResult> Index()
        {
              return _context.Honorentrytypes != null ? 
                          View(await _context.Honorentrytypes.ToListAsync()) :
                          Problem("Entity set 'ZerodropContext.Honorentrytypes'  is null.");
        }

        // GET: HonorEntryTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Honorentrytypes == null)
            {
                return NotFound();
            }

            var honorentrytype = await _context.Honorentrytypes
                .FirstOrDefaultAsync(m => m.IdHonorEntryType == id);
            if (honorentrytype == null)
            {
                return NotFound();
            }

            return View(honorentrytype);
        }

        // GET: HonorEntryTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HonorEntryTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHonorEntryType,Type,DefaultPoints,Description,ExpirationDate,HonorEntryTypecol")] Honorentrytype honorentrytype)
        {
            if (ModelState.IsValid)
            {
                _context.Add(honorentrytype);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(honorentrytype);
        }

        // GET: HonorEntryTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Honorentrytypes == null)
            {
                return NotFound();
            }

            var honorentrytype = await _context.Honorentrytypes.FindAsync(id);
            if (honorentrytype == null)
            {
                return NotFound();
            }
            return View(honorentrytype);
        }

        // POST: HonorEntryTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHonorEntryType,Type,DefaultPoints,Description,ExpirationDate,HonorEntryTypecol")] Honorentrytype honorentrytype)
        {
            if (id != honorentrytype.IdHonorEntryType)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(honorentrytype);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HonorentrytypeExists(honorentrytype.IdHonorEntryType))
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
            return View(honorentrytype);
        }

        // GET: HonorEntryTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Honorentrytypes == null)
            {
                return NotFound();
            }

            var honorentrytype = await _context.Honorentrytypes
                .FirstOrDefaultAsync(m => m.IdHonorEntryType == id);
            if (honorentrytype == null)
            {
                return NotFound();
            }

            return View(honorentrytype);
        }

        // POST: HonorEntryTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Honorentrytypes == null)
            {
                return Problem("Entity set 'ZerodropContext.Honorentrytypes'  is null.");
            }
            var honorentrytype = await _context.Honorentrytypes.FindAsync(id);
            if (honorentrytype != null)
            {
                _context.Honorentrytypes.Remove(honorentrytype);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HonorentrytypeExists(int id)
        {
          return (_context.Honorentrytypes?.Any(e => e.IdHonorEntryType == id)).GetValueOrDefault();
        }
    }
}
