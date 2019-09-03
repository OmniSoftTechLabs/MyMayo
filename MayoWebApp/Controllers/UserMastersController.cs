﻿using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MayoWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMastersController : ControllerBase
    {
        private readonly MyMayoContext _context;

        public UserMastersController(MyMayoContext context)
        {
            _context = context;
        }

        // GET: api/UserMasters
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<UserMaster>>> GetUserMaster()
        {
            return await _context.UserMaster.ToListAsync();
        }

        // GET: api/UserMasters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserMaster>> GetUserMaster(Guid id)
        {
            var userMaster = await _context.UserMaster.FindAsync(id);

            if (userMaster == null)
            {
                return NotFound();
            }

            return userMaster;
        }

        // PUT: api/UserMasters/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserMaster(Guid id, UserMaster userMaster)
        {
            if (id != userMaster.UserId)
            {
                return BadRequest();
            }

            _context.Entry(userMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserMasterExists(id))
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

        // POST: api/UserMasters
        [HttpPost]
        public async Task<ActionResult<UserMaster>> PostUserMaster(UserMaster userMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UserMaster.Add(userMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserMaster", new { id = userMaster.UserId }, userMaster);
        }

        // POST: api/UserMasters/AddTeacherUserMaster
        [HttpPost("[action]")]
        public async Task<ActionResult<UserMaster>> AddTeacherUserMaster(UserMaster userMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            userMaster.RoleId = _context.RoleMaster.FirstOrDefault(p => p.RoleName == "Teacher").RoleId;

            _context.UserMaster.Add(userMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserMaster", new { id = userMaster.UserId }, userMaster);
        }

        // DELETE: api/UserMasters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserMaster>> DeleteUserMaster(Guid id)
        {
            var userMaster = await _context.UserMaster.FindAsync(id);
            if (userMaster == null)
            {
                return NotFound();
            }

            _context.UserMaster.Remove(userMaster);
            await _context.SaveChangesAsync();

            return userMaster;
        }

        private bool UserMasterExists(Guid id)
        {
            return _context.UserMaster.Any(e => e.UserId == id);
        }
    }
}