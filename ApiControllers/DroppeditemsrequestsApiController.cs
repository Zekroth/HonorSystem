using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HonorSystem.sakila;

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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDroppeditemsrequest(int id, Droppeditemsrequest droppeditemsrequest)
        {
            if (id != droppeditemsrequest.IdDroppedItemsRequests)
            {
                return BadRequest();
            }

            _context.Entry(droppeditemsrequest).State = EntityState.Modified;

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

            return NoContent();
        }

        // POST: api/DroppeditemsrequestsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Droppeditemsrequest>> PostDroppeditemsrequest(Droppeditemsrequest droppeditemsrequest)
        {
            var found = _context.Find<Droppeditemsrequest>(droppeditemsrequest.IdLeftItemInGuildStorage, droppeditemsrequest.IdMember);

            if (found != null)
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
