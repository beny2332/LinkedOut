using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkedOut.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProtectedController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> ProtectedRoute() => Ok("Protected route");
    }
}
