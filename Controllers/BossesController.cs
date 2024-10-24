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
    public class BossesController : Controller
    {
        private readonly EvildogsContext _context;

        public BossesController(EvildogsContext context)
        {
            _context = context;
        }

        // GET: Bosses
        public async Task<IActionResult> Index()
        {
              return _context.Bosses != null ? 
                          View(await _context.Bosses.ToListAsync()) :
                          Problem("Entity set 'EvildogsContext.Bosses'  is null.");
        }

        // GET: Bosses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bosses == null)
            {
                return NotFound();
            }

            var boss = await _context.Bosses
                .FirstOrDefaultAsync(m => m.IdBoss == id);
            if (boss == null)
            {
                return NotFound();
            }

            return View(boss);
        }

        // GET: Bosses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bosses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdBoss,BossName,IsField,IsArch,IsGuild")] Boss boss)
        {
            if (ModelState.IsValid)
            {
                _context.Add(boss);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(boss);
        }

        // GET: Bosses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bosses == null)
            {
                return NotFound();
            }

            var boss = await _context.Bosses.FindAsync(id);
            if (boss == null)
            {
                return NotFound();
            }
            return View(boss);
        }

        // POST: Bosses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdBoss,BossName,IsField,IsArch,IsGuild")] Boss boss)
        {
            if (id != boss.IdBoss)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(boss);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BossExists(boss.IdBoss))
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
            return View(boss);
        }

        // GET: Bosses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bosses == null)
            {
                return NotFound();
            }

            var boss = await _context.Bosses
                .FirstOrDefaultAsync(m => m.IdBoss == id);
            if (boss == null)
            {
                return NotFound();
            }

            return View(boss);
        }

        // POST: Bosses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bosses == null)
            {
                return Problem("Entity set 'EvildogsContext.Bosses'  is null.");
            }
            var boss = await _context.Bosses.FindAsync(id);
            if (boss != null)
            {
                _context.Bosses.Remove(boss);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BossExists(int id)
        {
          return (_context.Bosses?.Any(e => e.IdBoss == id)).GetValueOrDefault();
        }
    }
}
