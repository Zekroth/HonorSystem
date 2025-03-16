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
    public class ItemRequestsController : Controller
    {
        private readonly ZerodropContext _context;

        public ItemRequestsController(ZerodropContext context)
        {
            _context = context;
        }

        // GET: ItemRequests
        public async Task<IActionResult> Index()
        {
            var ZerodropContext = _context.Itemrequests.Include(i => i.Item).Include(i => i.Player);
            return View(await ZerodropContext.ToListAsync());
        }

        // GET: ItemRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Itemrequests == null)
            {
                return NotFound();
            }

            var itemrequest = await _context.Itemrequests
                .Include(i => i.Item)
                .Include(i => i.Player)
                .FirstOrDefaultAsync(m => m.IdItemRequest == id);
            if (itemrequest == null)
            {
                return NotFound();
            }

            return View(itemrequest);
        }

        // GET: ItemRequests/Create
        public IActionResult Create()
        {
            ViewData["ItemId"] = new SelectList(_context.Items, "IdItem", "IdItem");
            ViewData["PlayerId"] = new SelectList(_context.Members, "IdMembers", "IdMembers");
            return View();
        }

        // POST: ItemRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdItemRequest,IsSupplied,ItemId,PlayerId")] Itemrequest itemrequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemrequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemId"] = new SelectList(_context.Items, "IdItem", "IdItem", itemrequest.ItemId);
            ViewData["PlayerId"] = new SelectList(_context.Members, "IdMembers", "IdMembers", itemrequest.PlayerId);
            return View(itemrequest);
        }

        // GET: ItemRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Itemrequests == null)
            {
                return NotFound();
            }

            var itemrequest = await _context.Itemrequests.FindAsync(id);
            if (itemrequest == null)
            {
                return NotFound();
            }
            ViewData["ItemId"] = new SelectList(_context.Items, "IdItem", "IdItem", itemrequest.ItemId);
            ViewData["PlayerId"] = new SelectList(_context.Members, "IdMembers", "IdMembers", itemrequest.PlayerId);
            return View(itemrequest);
        }

        // POST: ItemRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdItemRequest,IsSupplied,ItemId,PlayerId")] Itemrequest itemrequest)
        {
            if (id != itemrequest.IdItemRequest)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemrequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemrequestExists(itemrequest.IdItemRequest))
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
            ViewData["ItemId"] = new SelectList(_context.Items, "IdItem", "IdItem", itemrequest.ItemId);
            ViewData["PlayerId"] = new SelectList(_context.Members, "IdMembers", "IdMembers", itemrequest.PlayerId);
            return View(itemrequest);
        }

        // GET: ItemRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Itemrequests == null)
            {
                return NotFound();
            }

            var itemrequest = await _context.Itemrequests
                .Include(i => i.Item)
                .Include(i => i.Player)
                .FirstOrDefaultAsync(m => m.IdItemRequest == id);
            if (itemrequest == null)
            {
                return NotFound();
            }

            return View(itemrequest);
        }

        // POST: ItemRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Itemrequests == null)
            {
                return Problem("Entity set 'ZerodropContext.Itemrequests'  is null.");
            }
            var itemrequest = await _context.Itemrequests.FindAsync(id);
            if (itemrequest != null)
            {
                _context.Itemrequests.Remove(itemrequest);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemrequestExists(int id)
        {
          return (_context.Itemrequests?.Any(e => e.IdItemRequest == id)).GetValueOrDefault();
        }
    }
}
