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
        private readonly JwtService jwtService;
        public UserController(UserService _userService, JwtService _jwtService) 
        {
            userService = _userService; 
            jwtService = _jwtService;
        }

        // get a user by their id.
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SingleUserResponseDTO>> getUser(int id)
        {
            UserModel user = await userService.GetUserById(id);
            return user != null ? Ok(user) : NotFound();
        }

        // register/create a new user.
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

        // login and get token
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<string>> login([FromBody] UserModel user)
        {
            UserModel userFromDb = await userService.GetUserByUserNameAndPassword(user.userName, user.password);
            if (userFromDb == null)
            {
                return Unauthorized("Invalid user name or password");
            }
            string token = jwtService.genJWToken(userFromDb);
            return Ok(token);
        }
    }
}
