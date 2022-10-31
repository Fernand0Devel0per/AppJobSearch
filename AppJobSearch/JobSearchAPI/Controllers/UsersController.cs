using JobSearchAPI.Database;
using JobSearchDomain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace JobSearchAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private JobSearchContext _context;
        public UsersController(JobSearchContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserAsync(string email, string password)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                return NotFound();
            }
            return new JsonResult(user);
        }

        [HttpPost] 
        public async Task<IActionResult> AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return Created("http://localhost:26042/users", user);
            
        }
    }
}
