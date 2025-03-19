using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HonorSystem.sakila;
using Microsoft.EntityFrameworkCore.Internal;

namespace HonorSystem.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassificaApiController : ControllerBase
    {
        private readonly ZerodropContext _context;

        public ClassificaApiController(ZerodropContext context)
        {
            _context = context;
        }

        // GET: api/DroppedItemsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassificaWithId>>> GetClassifica([FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 0)
        {
            var items =
                from Classifica in _context.Classificas
                join Members in _context.Members on Classifica.Name equals Members.Name into ClassificaMembers
                from CM in ClassificaMembers.DefaultIfEmpty()
                where CM.IsStillInGuild == 1 && CM.IsActive == 1
                select new { c = Classifica, u = CM };

            List<ClassificaWithId> cla = await items
                .OrderByDescending((x) => x.c.TotalePunti)
                .Select(x => new ClassificaWithId()
                {
                    IdMember = x.u.IdMembers,
                    Name = x.u.Name,
                    TotalePunti = x.c.TotalePunti
                })
                .ToListAsync();

            if (pageNumber == 0 && pageSize == 0)
            {
                return Ok(cla);
            }
            else
            {
                return Ok(cla
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize));
            }
        }
    }
}
