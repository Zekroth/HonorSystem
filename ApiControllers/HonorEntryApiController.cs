using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using HonorSystem.sakila;

namespace HonorSystem.ApiControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HonorentryApiController : ControllerBase
    {
        [HttpGet]
        [SwaggerOperation(Summary = "Gets all honor entries")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns all honor entries")]
        public ActionResult<IEnumerable<Honorentry>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            using (var context = new EvildogsContext())
            {
                var honorEntries = context.Honorentries
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                return Ok(honorEntries);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Gets a specific honor entry by ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns the honor entry with the specified ID")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Honor entry not found")]
        public ActionResult<Honorentry> GetById(int id)
        {
            using (var context = new EvildogsContext())
            {
                var honorEntry = context.Honorentries.Find(id);
                if (honorEntry == null)
                {
                    return NotFound();
                }
                return Ok(honorEntry);
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Creates a new honor entry")]
        [SwaggerResponse(StatusCodes.Status201Created, "Honor entry created successfully")]
        public ActionResult<Honorentry> Create([FromBody] Honorentry honorEntry)
        {
            using (var context = new EvildogsContext())
            {
                context.Honorentries.Add(honorEntry);
                context.SaveChanges();
                return CreatedAtAction(nameof(GetById), new { id = honorEntry.IdHonorEntry }, honorEntry);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Updates an existing honor entry")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Honor entry updated successfully")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Honor entry not found")]
        public IActionResult Update(int id, [FromBody] Honorentry honorEntry)
        {
            if (id != honorEntry.IdHonorEntry)
            {
                return BadRequest();
            }

            using (var context = new EvildogsContext())
            {
                var existingEntry = context.Honorentries.Find(id);
                if (existingEntry == null)
                {
                    return NotFound();
                }

                context.Entry(existingEntry).CurrentValues.SetValues(honorEntry);
                context.SaveChanges();
                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes an honor entry")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Honor entry deleted successfully")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Honor entry not found")]
        public IActionResult Delete(int id)
        {
            using (var context = new EvildogsContext())
            {
                var honorEntry = context.Honorentries.Find(id);
                if (honorEntry == null)
                {
                    return NotFound();
                }

                context.Honorentries.Remove(honorEntry);
                context.SaveChanges();
                return NoContent();
            }
        }
    }
}
