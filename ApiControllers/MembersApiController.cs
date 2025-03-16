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
    public class MembersApiController : ControllerBase
    {
        private readonly ZerodropContext _context;

        public MembersApiController(ZerodropContext context)
        {
            _context = context;
        }

        // GET: api/MembersApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var members = await _context.Members
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(members);
        }

        // GET: api/MembersApi
        [HttpGet]
        [Route("GetAllMembers")]
        public async Task<ActionResult<IEnumerable<Member>>> GetAllMembers()
        {
            var members = await _context.Members
                .ToListAsync();

            return Ok(members);
        }

        // GET: api/MembersApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(int id)
        {
            var member = await _context.Members.FindAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            return member;
        }

        // PUT: api/MembersApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMember(int id, Member member)
        {
            if (id != member.IdMembers)
            {
                return BadRequest();
            }

            _context.Entry(member).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(id))
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

        // POST: api/MembersApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Member>> PostMember(Member member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMember", new { id = member.IdMembers }, member);
        }

        /// <summary>
        ///     POST: api/MembersApi
        /// </summary>
        /// <param name="member"></param>
        /// <remarks>USAGE: Only pass Member with EITHER the Name parameter OR the SecondaryCharacterName parameter</remarks>
        /// <returns></returns>
        [HttpPost]
        [Route("RegisterFromDiscord")]
        public async Task<ActionResult<Member>> RegisterFromDiscord(Member member)
        {
            var e = _context.Members.Where(x => x.Name == member.Name);

            if ((member.Name != null && member.Name != ""))
            {
                if (e.Count() == 1)
                {
                    if (e.First().CharacterName != member.CharacterName || e.First().SecondaryCharacterName != member.SecondaryCharacterName)
                    {
                        e.First().CharacterName = member.CharacterName;
                        e.First().SecondaryCharacterName = member.SecondaryCharacterName ?? e.First().SecondaryCharacterName;

                        _context.Entry(e.First()).State = EntityState.Modified;
                    }
                    else
                        return StatusCode(304);
                }
                else
                {
                    _context.Members.Add(member);
                }
            }
            else {
                return BadRequest();
            }


                await _context.SaveChangesAsync();

            return CreatedAtAction("GetMember", new { id = member.IdMembers }, member);
        }

        // DELETE: api/MembersApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.IdMembers == id);
        }
    }
}
