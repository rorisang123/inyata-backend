using Microsoft.AspNetCore.Mvc;

namespace inyata_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        private static readonly List<User> Users = new();

        // POST: api/users  
        [HttpPost]
        public IActionResult CreateUser([FromBody] User newUser)
        {
            if (newUser == null)
            {
                return BadRequest("User data is invalid.");
            }

            newUser.Id = Users.Count > 0 ? Users.Max(u => u.Id) + 1 : 1;
            Users.Add(newUser);
            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            return Ok(user);
        }
    }
}
