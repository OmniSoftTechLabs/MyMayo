using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Models;

namespace MayoWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleMastersController : ControllerBase
    {
        private readonly MyMayoContext _context;

        public RoleMastersController(MyMayoContext context)
        {
            _context = context;
        }

        // GET: api/RoleMasters/GetRoleMaster
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<RoleMaster>>> GetRoleMaster()
        {
            return await _context.RoleMaster.ToListAsync();
        }

        // GET: api/RoleMasters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleMaster>> GetRoleMaster(int id)
        {
            var roleMaster = await _context.RoleMaster.FindAsync(id);

            if (roleMaster == null)
            {
                return NotFound();
            }

            return roleMaster;
        }

        // PUT: api/RoleMasters/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoleMaster(int id, RoleMaster roleMaster)
        {
            if (id != roleMaster.RoleId)
            {
                return BadRequest();
            }

            _context.Entry(roleMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleMasterExists(id))
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

        // POST: api/RoleMasters
        [HttpPost]
        public async Task<ActionResult<RoleMaster>> PostRoleMaster(RoleMaster roleMaster)
        {
            _context.RoleMaster.Add(roleMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoleMaster", new { id = roleMaster.RoleId }, roleMaster);
        }

        // DELETE: api/RoleMasters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RoleMaster>> DeleteRoleMaster(int id)
        {
            var roleMaster = await _context.RoleMaster.FindAsync(id);
            if (roleMaster == null)
            {
                return NotFound();
            }

            _context.RoleMaster.Remove(roleMaster);
            await _context.SaveChangesAsync();

            return roleMaster;
        }

        private bool RoleMasterExists(int id)
        {
            return _context.RoleMaster.Any(e => e.RoleId == id);
        }
    }
}
