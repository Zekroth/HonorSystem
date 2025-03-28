﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HonorSystem.sakila;
using Microsoft.IdentityModel.Tokens;

namespace HonorSystem.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DroppeditemsrequestsApiController : ControllerBase
    {
        private readonly ZerodropContext _context;

        public DroppeditemsrequestsApiController(ZerodropContext context)
        {
            _context = context;
        }

        // GET: api/DroppeditemsrequestsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Droppeditemsrequest>>> GetDroppeditemsrequests()
        {
            return await _context.Droppeditemsrequests
                .Include(x => x.IdMemberNavigation)
                .Include(x => x.IdLeftItemInGuildStorageNavigation)
                .Include(x => x.IdLeftItemInGuildStorageNavigation.IdItemNavigation)
                .Where(x => x.IdLeftItemInGuildStorageNavigation.DistributedTo == null)
                .ToListAsync();
        }

        // GET: api/DroppeditemsrequestsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Droppeditemsrequest>> GetDroppeditemsrequest(int id)
        {
            var droppeditemsrequest = _context.Droppeditemsrequests
                .Include(x => x.IdMemberNavigation)
                .Include(x => x.IdLeftItemInGuildStorageNavigation)
                .Include(x => x.IdLeftItemInGuildStorageNavigation.IdItemNavigation)
                .FirstOrDefault(x => x.IdLeftItemInGuildStorage == id);

            if (droppeditemsrequest == null)
            {
                return NotFound();
            }

            return droppeditemsrequest;
        }

        // PUT: api/DroppeditemsrequestsApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("id")]
        public async Task<IActionResult> PutDroppeditemsrequest(int id, Droppeditemsrequest droppeditemsrequest)
        {
            if (id != droppeditemsrequest.IdDroppedItemsRequests)
            {
                return BadRequest();
            }

            // Aggiorna la data di richiesta se è stata modificata
            var existingRequest = await _context.Droppeditemsrequests
                .FirstOrDefaultAsync(x => x.IdDroppedItemsRequests == id);

            if (existingRequest != null)
            {
                // Verifica se la data è stata modificata
                if (droppeditemsrequest.RequestDate != existingRequest.RequestDate)
                {
                    // Se modificata, aggiorna la data
                    existingRequest.RequestDate = droppeditemsrequest.RequestDate;
                }

                // Imposta lo stato modificato per l'entità
                _context.Entry(existingRequest).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DroppeditemsrequestExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return NoContent();
        }

        // POST: api/DroppeditemsrequestsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Droppeditemsrequest>> PostDroppeditemsrequest(Droppeditemsrequest droppeditemsrequest)
        {
            var found = _context.Droppeditemsrequests.Where( x => x.IdLeftItemInGuildStorage == droppeditemsrequest.IdLeftItemInGuildStorage && x.IdMember == droppeditemsrequest.IdMember);

            if (!found.IsNullOrEmpty())
            {
                return StatusCode(409, found);
            }

            _context.Droppeditemsrequests.Add(droppeditemsrequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDroppeditemsrequest", new { id = droppeditemsrequest.IdDroppedItemsRequests }, droppeditemsrequest);
        }

        // DELETE: api/DroppeditemsrequestsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDroppeditemsrequest(int id)
        {
            var droppeditemsrequest = await _context.Droppeditemsrequests.FindAsync(id);
            if (droppeditemsrequest == null)
            {
                return NotFound();
            }

            _context.Droppeditemsrequests.Remove(droppeditemsrequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DroppeditemsrequestExists(int id)
        {
            return _context.Droppeditemsrequests.Any(e => e.IdDroppedItemsRequests == id);
        }
    }
}
