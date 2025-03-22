﻿using System;
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
    public class DroppedItemsApiController : ControllerBase
    {
        private readonly ZerodropContext _context;

        public DroppedItemsApiController(ZerodropContext context)
        {
            _context = context;
        }

        // GET: api/DroppedItemsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Leftiteminguildstorage>>> GetLeftiteminguildstorages([FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 0)
        {
            var items = new List<Leftiteminguildstorage>();
            if (pageNumber == 0 && pageSize == 0)
            {
                items = await _context.Leftiteminguildstorages
                .ToListAsync();
            } else
            {
                items = await _context.Leftiteminguildstorages
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            }
            return Ok(items);
        }
        // GET: api/GetAllDroppedItems
        [HttpGet]
        [Route("GetAllDroppedItems")]
        public async Task<ActionResult<IEnumerable<Leftiteminguildstorage>>> GetAllDroppedItems()
        {
            var items = await _context.Leftiteminguildstorages
                .ToListAsync();

            return Ok(items);
        }

        // GET: api/AvailableDroppedItemsApi
        [HttpGet]
        [Route("AvailableDroppedItems")]
        public async Task<ActionResult<IEnumerable<Leftiteminguildstorage>>> AvailableDroppedItems([FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 0)
        {
            if (_context.Leftiteminguildstorages == null)
            {
                return NotFound("Leftiteminguildstorages not found.");
            }

            if (_context.Leftiteminguildstorages
                .Where(x => x.DistributedDate == null).Count() == 0)
            {
                return StatusCode(204);
            }

            List<Leftiteminguildstorage> items;

            if (pageNumber == 0 && pageSize == 0)
            {
                items = await _context.Leftiteminguildstorages
                    .Include(x => x.IdHonorEntryNavigation)
                    .Include(x => x.IdItemNavigation)
                    .Where(x => x.DistributedDate == null)
                    .ToListAsync();
            }
            else
            {
                items = await _context.Leftiteminguildstorages
                    .Include(x => x.IdHonorEntryNavigation)
                    .Include(x => x.IdItemNavigation)
                    .Where(x => x.DistributedDate == null)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }

            return Ok(items);
        }


        // GET: api/DroppedItemsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Leftiteminguildstorage>> GetLeftiteminguildstorage(int id)
        {
            var leftiteminguildstorage = await _context.Leftiteminguildstorages.FindAsync(id);

            if (leftiteminguildstorage == null)
            {
                return NotFound();
            }

            return leftiteminguildstorage;
        }

        // PUT: api/DroppedItemsApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeftiteminguildstorage(int id, Leftiteminguildstorage leftiteminguildstorage)
        {
            if (id != leftiteminguildstorage.Id)
            {
                return BadRequest();
            }

            _context.Entry(leftiteminguildstorage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeftiteminguildstorageExists(id))
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

        // POST: api/DroppedItemsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Leftiteminguildstorage>> PostLeftiteminguildstorage(Leftiteminguildstorage leftiteminguildstorage)
        {
            _context.Leftiteminguildstorages.Add(leftiteminguildstorage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLeftiteminguildstorage", new { id = leftiteminguildstorage.Id }, leftiteminguildstorage);
        }

        // DELETE: api/DroppedItemsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeftiteminguildstorage(int id)
        {
            var leftiteminguildstorage = await _context.Leftiteminguildstorages.FindAsync(id);
            if (leftiteminguildstorage == null)
            {
                return NotFound();
            }

            _context.Leftiteminguildstorages.Remove(leftiteminguildstorage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LeftiteminguildstorageExists(int id)
        {
            return _context.Leftiteminguildstorages.Any(e => e.Id == id);
        }
    }
}
