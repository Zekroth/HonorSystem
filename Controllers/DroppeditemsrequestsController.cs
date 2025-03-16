using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HonorSystem.sakila;
using HonorSystem.Pages.Droppeditemsrequests;

namespace HonorSystem.Controllers
{
    public class DroppeditemsrequestsController : Controller
    {
        private readonly ZerodropContext _context;

        public DroppeditemsrequestsController(ZerodropContext context)
        {
            _context = context;
        }

        // GET: Droppeditemsrequests
        public async Task<IActionResult> Index(int pageNumber = 0, int pageSize = 0)
        {
            var zerodropContext = _context.Droppeditemsrequests
                .Include(d => d.IdLeftItemInGuildStorageNavigation)
                .Include(d => d.IdMemberNavigation)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            var totalItems = await _context.Droppeditemsrequests.CountAsync();
            var viewModel = new IndexModel(_context);

            return View(viewModel);
        }


        // GET: Droppeditemsrequests/Create
        public IActionResult Create()
        {
            ViewData["IdLeftItemInGuildStorage"] = new SelectList(_context.Leftiteminguildstorages, "Id", "Id");
            ViewData["IdMember"] = new SelectList(_context.Members, "IdMembers", "IdMembers");
            return View();
        }

        // POST: Droppeditemsrequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDroppedItemsRequests,RequestDate,IdMember,IdLeftItemInGuildStorage,Reason")] Droppeditemsrequest droppeditemsrequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(droppeditemsrequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdLeftItemInGuildStorage"] = new SelectList(_context.Leftiteminguildstorages, "Id", "Id", droppeditemsrequest.IdLeftItemInGuildStorage);
            ViewData["IdMember"] = new SelectList(_context.Members, "IdMembers", "IdMembers", droppeditemsrequest.IdMember);
            return View(droppeditemsrequest);
        }

        // GET: Droppeditemsrequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var droppeditemsrequest = await _context.Droppeditemsrequests.FindAsync(id);
            if (droppeditemsrequest == null)
            {
                return NotFound();
            }
            ViewData["IdLeftItemInGuildStorage"] = new SelectList(_context.Leftiteminguildstorages, "Id", "Id", droppeditemsrequest.IdLeftItemInGuildStorage);
            ViewData["IdMember"] = new SelectList(_context.Members, "IdMembers", "IdMembers", droppeditemsrequest.IdMember);
            return View(droppeditemsrequest);
        }

        // POST: Droppeditemsrequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDroppedItemsRequests,RequestDate,IdMember,IdLeftItemInGuildStorage,Reason")] Droppeditemsrequest droppeditemsrequest)
        {
            if (id != droppeditemsrequest.IdDroppedItemsRequests)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(droppeditemsrequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DroppeditemsrequestExists(droppeditemsrequest.IdDroppedItemsRequests))
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
            ViewData["IdLeftItemInGuildStorage"] = new SelectList(_context.Leftiteminguildstorages, "Id", "Id", droppeditemsrequest.IdLeftItemInGuildStorage);
            ViewData["IdMember"] = new SelectList(_context.Members, "IdMembers", "IdMembers", droppeditemsrequest.IdMember);
            return View(droppeditemsrequest);
        }

        // GET: Droppeditemsrequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var droppeditemsrequest = await _context.Droppeditemsrequests
                .Include(d => d.IdLeftItemInGuildStorageNavigation)
                .Include(d => d.IdMemberNavigation)
                .FirstOrDefaultAsync(m => m.IdDroppedItemsRequests == id);
            if (droppeditemsrequest == null)
            {
                return NotFound();
            }

            return View(droppeditemsrequest);
        }

        // POST: Droppeditemsrequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var droppeditemsrequest = await _context.Droppeditemsrequests.FindAsync(id);
            if (droppeditemsrequest != null)
            {
                _context.Droppeditemsrequests.Remove(droppeditemsrequest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Altri metodi rimangono invariati...

        private bool DroppeditemsrequestExists(int id)
        {
            return _context.Droppeditemsrequests.Any(e => e.IdDroppedItemsRequests == id);
        }
    }
}
