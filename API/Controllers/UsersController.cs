using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // https://localhost:5001/api/users
    public class UsersController : ControllerBase
    {
        // Field that can be used with all methods.
        // Allows us to access our database.
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        // Returns a list of users.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            // Returns a list of users.
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")] // https://localhost:5001/api/users/{id}
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            // Returns a user with the specified id.
            return await _context.Users.FindAsync(id);
        }
    }
}
