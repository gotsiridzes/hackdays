using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Koggo.Domain.Models;
using Koggo.Infrastructure;

namespace Koggo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserServicesController : ControllerBase
    {
        private readonly KoggoDbContext _context;

        public UserServicesController(KoggoDbContext context)
        {
            _context = context;
        }

        // GET: api/UserServices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserService>>> GetUserService()
        {
          if (_context.UserServices == null)
          {
              return NotFound();
          }
            return await _context.UserServices.ToListAsync();
        }

        // GET: api/UserServices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserService>> GetUserService(int id)
        {
          if (_context.UserServices == null)
          {
              return NotFound();
          }
            var userService = await _context.UserServices.FindAsync(id);

            if (userService == null)
            {
                return NotFound();
            }

            return userService;
        }

        // PUT: api/UserServices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserService(int id, UserService userService)
        {
            if (id != userService.Id)
            {
                return BadRequest();
            }

            _context.Entry(userService).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserServiceExists(id))
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

        // POST: api/UserServices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserService>> PostUserService(UserService userService)
        {
          if (_context.UserServices == null)
          {
              return Problem("Entity set 'KoggoTestContext.UserService'  is null.");
          }
            _context.UserServices.Add(userService);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserService", new { id = userService.Id }, userService);
        }

        // DELETE: api/UserServices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserService(int id)
        {
            if (_context.UserServices == null)
            {
                return NotFound();
            }
            var userService = await _context.UserServices.FindAsync(id);
            if (userService == null)
            {
                return NotFound();
            }

            _context.UserServices.Remove(userService);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserServiceExists(int id)
        {
            return (_context.UserServices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
