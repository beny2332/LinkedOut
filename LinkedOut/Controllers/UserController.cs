using LinkedOut.DTO;
using LinkedOut.Models;
using LinkedOut.Services;
using Microsoft.AspNetCore.Mvc;

namespace LinkedOut.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;
        public UserController(UserService _userService) { userService = _userService; }

        // get requst to get a user by their id.
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SingleUserResponseDTO>> getUser(int id)
        {
            UserModel user = await userService.GetUserById(id);
            return user != null ? Ok(user) : NotFound();
        }
        // post requst to register/create a new user.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> register([FromBody] UserModel user)
        {
            int userId = await userService.register(user);
            if (userId != 0)
            {
                //Response.StatusCode = StatusCodes.Status201Created;
                //await Response.WriteAsync(userId.ToString());
                return Ok(userId);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
