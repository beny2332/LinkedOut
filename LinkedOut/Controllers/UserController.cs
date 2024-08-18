using LinkedOut.DTO;
using LinkedOut.Models;
using LinkedOut.Services;
using Microsoft.AspNetCore.Mvc;

namespace LinkedOut.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;
        public UserController(UserService _userService) {userService = _userService;}

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SingleUserResponseDTO>> getUser(int id)
        {
            UserModel user = await userService.GetUserById(id);
            return user != null ? Ok(user) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SingleUserResponseDTO>> register([FromBody] UserModel user)
        {
            //UserModel user = await userService.GetUserById(id);
            return user != null ? Ok(user) : NotFound();
        }
    }
}
