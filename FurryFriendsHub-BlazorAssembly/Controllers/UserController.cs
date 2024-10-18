using Amazon.Runtime.Internal.Auth;
using FurryFriendsHub_BlazorAssembly.Shared;
using FurryFriendsHub_BlazorAssembly.Repositories;
using Microsoft.AspNetCore.Mvc;
using Amazon.Runtime.Internal.Transform;

namespace FurryFriendsHub_BlazorAssembly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private IUserCollection db = new UserCollection();

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await db.GetAllUsers());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserDetails(string id)
        {
            return Ok(await db.GetUserById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(user.email)) 
            {
                ModelState.AddModelError("Email", "Se Requiere el Email");
                return BadRequest(ModelState);
            }

            await db.InsertUser(user);

            return CreatedAtAction(nameof(CreateUser), new { id = user.Id }, user); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] User user, string id)
        {
            if (user == null)
            {
                return BadRequest();
            }

            if (user.email != null)
            {
                ModelState.AddModelError("Email", "Se Requiere el Email");
            }

            user.Id = id;
            await db.UpdateUser(user);

            return Created("Created", true);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletUser(string id)
        {
            await db.DeleteUser(id);
            return NoContent();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserAuthDTO userAuthDTO)
        {
            var userObject = await db.AuthenticateUser(userAuthDTO);

            if (userObject == null)
            {
                return Unauthorized("Usuario o contraseña incorrectos");
            }

            return Ok(userObject);

        }
    }
}
